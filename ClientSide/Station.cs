using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace ClientSide
{
    internal class Station
    {
        private static readonly object lockObject = new();
        private static string serverIP = "127.0.0.1"; // Replace with a real one
        private static int serverPort = 65009;

        private static Station instance;
        private User CurrentUser;
        private bool isUsingTemporaryPassword;

        private Station(){}
        public static void StartStation()
        {
            if (instance == null)
            {
                instance = new Station();
                instance.CurrentUser = new User("","","","",new DateTime() );            
            }
            
       }
        public static string GetCurrentUsername()
        { return instance.CurrentUser.UserName; }
        public static bool IsUsingTemporaryPassword { get { return instance.isUsingTemporaryPassword; } }
        //  public static void SetCurrentUser(User user) { instance.currentUser = user;  }
        private string ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            int totalBytesRead = 0;

            while (true)
            {
                int bytesRead = stream.Read(buffer, totalBytesRead, buffer.Length);
                totalBytesRead += bytesRead;
                if (buffer[totalBytesRead - 1] == 0) break;
            }

            string message = Encoding.UTF8.GetString(buffer, 0, totalBytesRead - 1);

            return message;
        }
        Func<NetworkStream, string> ReceiveMessageFunc = (stream) =>
        {
            byte[] buffer = new byte[1024];
            int totalBytesRead = 0;

            while (true)
            {
                int bytesRead = stream.Read(buffer, totalBytesRead, buffer.Length - totalBytesRead);
                totalBytesRead += bytesRead;
                if (buffer[totalBytesRead - 1] == 0) break;
            }
            
            string messageWithNull = Encoding.UTF8.GetString(buffer, 0, totalBytesRead - 1);

            return messageWithNull;
        };

        private static async Task<string> ReceiveMessagesAsync(NetworkStream stream)
        {
            byte[] buffer = new byte[4096];
            int totalBytesRead = 0;
            
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, totalBytesRead, buffer.Length - totalBytesRead);
                totalBytesRead += bytesRead;
                if (buffer[totalBytesRead - 1] == 0) break;
            }
            
            string messageWithNull = Encoding.UTF8.GetString(buffer, 0, totalBytesRead - 1);

            return messageWithNull;
        }
        public static void SendRegistrationRequest(User u, SignUpForm form)
        {// 0:::USERNAME:::P_HASH:::FULLNAME:::EMAIL:::DOB
            string datestr = u.DoB.Day + " " + u.DoB.Month + " " + u.DoB.Year;            

            string request =
                $"0:::{u.UserName}:::{u.PasswordHash}:::{u.FullName}:::{u.Email}:::{datestr}\0";
            Task.Run(async () =>
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync(serverIP, serverPort);                       
                        NetworkStream stream = client.GetStream();  
                        byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                        stream.Write(requestBytes, 0, requestBytes.Length);
                        string reply = await ReceiveMessagesAsync(stream);
                        string[] fields = reply.Split(":::");
                        if (fields[1].Equals("1")) // Successful
                        {
                            form.Invoke(() => { MessageBox.Show("Đăng kí tài khoản thành công!"); form.Close(); });                            
                        }
                        else
                        {
                            form.Invoke(() => MessageBox.Show("Tên đăng nhập đã tồn tại!\n Đăng kí không thành công."));                            
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Đã xảy ra lỗi, vui lòng thử lại.");
                }
            });
        }
        public static void SendChangePasswordRequest(string username, string newPassword, ChangePasswordForm form)
        {//3:::<USERNAME>:::<NEWPASSWORD>  3:::nhotkute88:::Daylamatkhaumoi123@
            string request =
                $"3:::{username}:::{newPassword}\0";
            Task.Run(async () =>
            {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(serverIP, serverPort);
                    NetworkStream stream = client.GetStream();
                    byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                    stream.Write(requestBytes, 0, requestBytes.Length);

                }
                instance.isUsingTemporaryPassword = false;
                form.Invoke(() =>{ MessageBox.Show($"Đổi mật khẩu thành công."); form.Close(); });
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Đã xảy ra lỗi, vui lòng thử lại.");
                }
            });
        }
        public static void SendForgotPasswordRequest(string username, string emailAddress, ForgotPasswordForm form)
        {//4:::<USERNAME>:::<EMAIL>  4:::nhotkute88:::22520222@gm.uit.edu.vn
            string request =
                $"4:::{username}:::{emailAddress}\0";
            Task.Run(async () =>
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync(serverIP, serverPort);
                        NetworkStream stream = client.GetStream();
                        byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                        stream.Write(requestBytes, 0, requestBytes.Length);
                        string reply = await ReceiveMessagesAsync(stream);
                        if (reply.Equals("1")) // Successful
                        {
                            form.Invoke(() => { MessageBox.Show("Hệ thống đã gửi hướng dẫn đăng nhập,\nVui lòng kiểm tra email của bạn!");
                            form.Close(); });
                        }
                        else
                        {
                            form.Invoke(() => MessageBox.Show("Thông tin đã nhập không hợp lệ!"));
                        }

                    }
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Đã xảy ra lỗi, vui lòng thử lại.");
                }
            });


        }

        public static void SendLoginRequest(string username, string pswd, LoginForm form)
        {
            if (username.Equals("11") ) //CHEAT, for testing
            {   
                form.Invoke(() => {
                    form.Hide(); form.ClearPasswordField();
                    new LoggedInForm("SUPERUSER", "SUPERUSER", username, form).Show();
                    
                });
                return;
            }
            string loginRequest = $"1:::{username}:::{pswd}\0";
            Task.Run(async () =>
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync(serverIP, serverPort);
                        NetworkStream stream = client.GetStream(); 
                        byte[] requestBytes = Encoding.UTF8.GetBytes((string)loginRequest);                        
                        stream.Write(requestBytes, 0, requestBytes.Length);
                        
                        string reply = await ReceiveMessagesAsync(stream);
                        
                        string[] fields = reply.Split(":::");
                        if (fields[1].Equals("1")) // Successful
                        {   // 1:::1:::FullName:::DoB:::UserName:::<1 or 0:IsUsingTemporaryPassword>
                            int x = int.Parse(fields[5]);
                            instance.isUsingTemporaryPassword = (x == 1);

                            instance.CurrentUser.UserName = fields[4];
                            form.Invoke(() => {
                            form.Hide(); form.ClearPasswordField();
                                new LoggedInForm(fields[4] + "\n" + fields[2], fields[3],username,form).Show(); });                                                   
                        }
                        if (fields[1].Equals("0"))
                        {
                            form.Invoke( () => MessageBox.Show("Wrong password!") );
                        }
                        if (fields[1].Equals("2"))
                        {
                            form.Invoke(() => MessageBox.Show("No such username"));
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Đã xảy ra lỗi, vui lòng thử lại.{e.Message}");
                }
            });
        }
        public static void SendLogoutRequest(string username)
        {
            string loginRequest = $"2:::{username}\0";
            Task.Run(async () =>
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync(serverIP, serverPort);
                        NetworkStream stream = client.GetStream();
                        byte[] requestBytes = Encoding.UTF8.GetBytes((string)loginRequest);
                        stream.Write(requestBytes, 0, requestBytes.Length);                
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Đã xảy ra lỗi, vui lòng thử lại.");
                }
            });


        }




        }
}
