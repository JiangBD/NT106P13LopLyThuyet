using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace TCPServer
{
    internal class Server
    {   private static string serverIP = "127.0.0.1"; // Replace with a real public IPv4 interface
        private static int serverPort = 65009;
        private static readonly object lockObject = new();
        private static Server instance;
        Dictionary<string, User> loggedInUsers, allUsers;
        Dictionary<string, string> temporaryPasswordUsers; // username -> temporarypassword
        private Server() 
        {
            loggedInUsers = new Dictionary<string, User>();            
        }
        public static void CreateServer()
        {
            if (instance == null)
            {
                instance = new Server();      
                instance.allUsers = SQLServerDatabaseAccessor.GetAllUsers(); 
                instance.temporaryPasswordUsers = new Dictionary<string, string>();
            }
        }
        public static async Task StartServer()
        {
            
                IPAddress ipAddress = IPAddress.Parse(serverIP);
                try
                {
                
                    TcpListener listener = new TcpListener(new IPEndPoint(ipAddress, serverPort));
                    listener.Start();
                    Console.WriteLine("Server started...");
                    while (true)
                    {
                    Console.WriteLine("Waiting for stations...");
                    TcpClient client = await listener.AcceptTcpClientAsync(); 
                    IPEndPoint endpoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    Console.WriteLine($"Client connected {endpoint.Address} {endpoint.Port}");
                    // Handle client connection asynchronously
                    _ = HandleClientAsync(client);  
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }           

        }
        private static async Task<string> ReceiveMessagesAsync(NetworkStream stream)
        {
            byte[] buffer = new byte[4096];
            int totalBytesRead = 0;
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, totalBytesRead, buffer.Length);
                totalBytesRead += bytesRead;
                if (buffer[totalBytesRead - 1] == 0) break;                              
            }
            string messageNoNull = Encoding.UTF8.GetString(buffer, 0, totalBytesRead - 1);

            return messageNoNull;
        }
        private static async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                // Start sending and receiving in parallel
                string message = await ReceiveMessagesAsync(stream);
            //    Console.WriteLine($"{message}");
                
                HandleMessage(message,client);                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Client error: {e.Message}");
            }          
        }
        private static void HandleMessage(string message, TcpClient client)
        {   
            Task.Run(() =>
            {   try
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                       if (message[0] == '0') HandleRegistrationRequest(message, stream);
                        if (message[0] == '1') HandleLoginRequest(message, stream);
                        if (message[0] == '2') HandleLogoutRequest(message);
                        if (message[0] == '3') HandleChangePasswordRequest(message);
                        if (message[0] == '4') HandleForgotPasswordRequest(message, stream);
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
                finally { client.Close(); }
            });
        }
        private static void HandleRegistrationRequest(string message, NetworkStream stream)
        {// 0:::USERNAME:::P_HASH:::FULLNAME:::EMAIL:::DOB            
                string[] fields = message.Split(":::");
                string username = fields[1]; string password = fields[2];
                string fullname = fields[3]; string email = fields[4];
                string dob = fields[5];
                string[] date = dob.Split(" "); // DD MM YYYY
                DateTime dt = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            lock (lockObject)
            {
                if (!instance.allUsers.ContainsKey(username))
                {
                    User u = new User(username,  SQLServerDatabaseAccessor.ComputeSha256Hash( password), fullname, email, dt);
                    SQLServerDatabaseAccessor.InsertNewUser(u);
                    instance.allUsers.Add(username, u);

                    byte[] successfulReplyBytes =
                        Encoding.UTF8.GetBytes("0:::1\0");
                    stream.Write(successfulReplyBytes, 0, successfulReplyBytes.Length);
                    Console.WriteLine($"New user created: {username}");
                }
                else
                {
                    byte[] failedReplyBytes =
                        Encoding.UTF8.GetBytes("0:::0\0");
                    Console.WriteLine($"New user creation failed, existed: {username}");
                    stream.Write(failedReplyBytes, 0, failedReplyBytes.Length);
                }
            }
        }
        private static void HandleLoginRequest(string message, NetworkStream stream)
        {//1:::USERNAME:::PLAINTEXTPASSWORD
            string[] fields = message.Split(":::"); 
            Dictionary<string,User> tempAllUsers = instance.allUsers;
            Dictionary<string,string> tempPswdUsers = instance.temporaryPasswordUsers;
            if (tempPswdUsers.ContainsKey(fields[1]) ) // is using temp pswd
            {   string receivedTempPswd = fields[2];
                if (tempPswdUsers[fields[1]].Equals(receivedTempPswd))
                {
                    tempPswdUsers.Remove(fields[1]);
                    User user = tempAllUsers[fields[1]];
                    if (!instance.loggedInUsers.ContainsKey(user.UserName))   instance.loggedInUsers.Add(user.UserName, user);
                    string reply = "1:::1:::" + user.FullName + ":::"// 1:::1:::FullName:::DoB:::UserName:::<1 or 0:IsUsingTemporaryPassword>
                        + user.DoB.Day + " " + user.DoB.Month + " " + user.DoB.Year + ":::" + user.UserName + ":::" + "1" + "\0";
                    byte[] buffer = Encoding.UTF8.GetBytes(reply);
                    stream.Write(buffer, 0, buffer.Length);
                    Console.WriteLine($"User logged in using temporary password: {fields[1]}");                
                }
                else
                {
                    string reply = "1:::0\0";
                    byte[] buffer = Encoding.UTF8.GetBytes(reply);
                    stream.Write(buffer, 0, buffer.Length);
                    Console.WriteLine("Wrong temporary password");
                }
                return;
            }
            
            if (tempAllUsers.ContainsKey(fields[1]) )
            {
                string loginHash = tempAllUsers[fields[1]].PasswordHash;
                string receivedHash = SQLServerDatabaseAccessor.ComputeSha256Hash(fields[2]);
                if (loginHash.Equals(receivedHash))
                {// 1:::1:::FullName:::DoB:::UserName:::<1 or 0:IsUsingTemporaryPassword>   
                    User user = tempAllUsers[fields[1]];
                    if (!instance.loggedInUsers.ContainsKey(user.UserName))
                        instance.loggedInUsers.Add(user.UserName, user);
                    string reply = "1:::1:::" + user.FullName + ":::" 
                        + user.DoB.Day + " " + user.DoB.Month + " " + user.DoB.Year + ":::" + user.UserName + ":::" + "0" + "\0";
                    byte[] buffer = Encoding.UTF8.GetBytes(reply);
                    stream.Write(buffer, 0, buffer.Length);
                    Console.WriteLine($"User logged in: {fields[1]}");
                }
                else
                {
                    string reply = "1:::0\0";
                    byte[] buffer = Encoding.UTF8.GetBytes(reply);
                    stream.Write(buffer, 0, buffer.Length);
                    Console.WriteLine("Wrong password");
                }
            }    
            else
            {
                string reply = "1:::2\0"; // No such username
                byte[] buffer = Encoding.UTF8.GetBytes(reply);
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("No such username");
            }
        }
        private static void HandleLogoutRequest(string message)
        {// 2:::USERNAME
            string[] fields = message.Split(":::");
            if (instance.loggedInUsers.ContainsKey(fields[1]))
            { instance.loggedInUsers.Remove(fields[1]); Console.WriteLine($"User logged out: {fields[1]}");  }
        }
        private static void HandleChangePasswordRequest(string message)
        {//3:::<USERNAME>:::<NEWPASSWORD>  3:::nhotkute88:::Daylamatkhaumoi123@
            string[] fields = message.Split(":::");
            string username = fields[1];
            string newPassword = fields[2];
            string newPasswordHash = SQLServerDatabaseAccessor.ComputeSha256Hash(newPassword);
            SQLServerDatabaseAccessor.UpdatePassword(username, newPasswordHash);
            instance.allUsers[username].PasswordHash = newPasswordHash;
        }
        private static void HandleForgotPasswordRequest(string message, NetworkStream stream)
        {//4:::<USERNAME>:::<EMAIL>  4:::nhotkute88:::22520122@gm.uit.edu.vn 
            string[] fields = message.Split(":::");
            string receivedUsername = fields[1]; string receivedEmailAddress = fields[2];
            if (!instance.allUsers.ContainsKey(receivedUsername))
            {// NONEXISTENT USER!!
                string reply = "0\0";
                byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
                stream.Write(replyBytes, 0, replyBytes.Length);
                return;
            }
            User user = instance.allUsers[receivedUsername];
            if (!user.Email.Equals(receivedEmailAddress) )
            {//User exists,yet username and mail address dont match!
                string reply = "0\0";
                byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
                stream.Write(replyBytes, 0, replyBytes.Length);
                return;
            }
            if (instance.temporaryPasswordUsers.ContainsKey(receivedUsername))
            {
                //User exists,yet username is found in temporaryPasswordUsers!!
                string reply = "0\0";
                byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
                stream.Write(replyBytes, 0, replyBytes.Length);
                return;
            }

            //Now, those fields match:
            string _reply = "1\0";
            byte[] _replyBytes = Encoding.UTF8.GetBytes(_reply);
            stream.Write(_replyBytes, 0, _replyBytes.Length);
            SendTemporaryPasswordEmail(user.UserName,user.Email);
        }
        private static void SendTemporaryPasswordEmail(string username, string emailAddress)
        {
            string temporaryPassword = GenerateRandomString(6);
            instance.temporaryPasswordUsers.Add(username, temporaryPassword);
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;

            string email = "youremailaddress@gmail.com"; //youremailaddress@gmail.com
            string appPassword = "your_app_password "; //your_app_password          

            string subject = "MẬT KHẨU TẠM THỜI";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(@$"<p>Mật khẩu tạm thời là {temporaryPassword}</p>");
            stringBuilder.Append(@$"<p>Quý khách sẽ được yêu cầu thay đổi mật khẩu ngay khi đăng nhập bằng mật khẩu trên.</p>");


            string paragraphs = stringBuilder.ToString();
            string htmlBody = @$"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>Image Layout with Table</title>
</head>
<body>
    <!-- Table structure starts -->
    <table border=""0"" width=""100%"" style=""text-align: center;"">
        <!-- Top row with one image -->
        <!-- Middle rows with paragraphs -->
        <tr>
            <td>{paragraphs}</td>
        </tr>
        <!-- Bottom row with three images -->
    </table>
</body>
</html>";
            try
            {
                // Create the email message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(email);
                mail.To.Add(new MailAddress(emailAddress));
                mail.Subject = subject;
                mail.IsBodyHtml = true;

                // Create the alternative view for HTML content
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
                mail.AlternateViews.Add(htmlView);

                // Set up the SMTP client
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(email, appPassword);
                    smtpClient.EnableSsl = true;
                    // Send the email
                    smtpClient.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

        }
        static string GenerateRandomString(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {   result[i] = chars[random.Next(chars.Length)];     }
            return new string(result);
        }
        public static void Close()
        {
            
        }





    }
}
