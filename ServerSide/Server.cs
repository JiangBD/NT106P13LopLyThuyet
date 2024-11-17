using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    internal class Server
    {   private static string serverIP = "127.0.0.1"; // Replace with a real public IPv4 interface
        private static int serverPort = 65009;
        private static readonly object lockObject = new();
        private static Server instance;
        Dictionary<string, User> loggedInUsers, allUsers;
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
                Console.WriteLine($"{message}");
                
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

            if (tempAllUsers.ContainsKey(fields[1]) )
            {
                string loginHash = tempAllUsers[fields[1]].PasswordHash;
                string receivedHash = SQLServerDatabaseAccessor.ComputeSha256Hash(fields[2]);
                if (loginHash.Equals(receivedHash))
                {// 1:::1(CORRECT BIT):::FULLNAME:::DOB   
                    User user = tempAllUsers[fields[1]];
                    if (!instance.loggedInUsers.ContainsKey(user.UserName))
                        instance.loggedInUsers.Add(user.UserName, user);
                    string reply = "1:::1:::" + user.FullName + ":::"
                        + user.DoB.Day + " " + user.DoB.Month + " " + user.DoB.Year + "\0";
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
        public static void Close()
        {
            
        }





    }
}
