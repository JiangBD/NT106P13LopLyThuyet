using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClientSide
{
    internal class Station
    {
        private static readonly object lockObject = new();
        private static string serverIP = "127.0.0.1"; // Replace with a real one
        private static int serverPort = 65009;
        //private static Station instance;

        //private Station(){}
        /*public static void StartStation()
        {
            if ( instance == null ) instance = new Station(); 
        }*/
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
                int bytesRead = stream.Read(buffer, totalBytesRead, buffer.Length);
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
                int bytesRead = await stream.ReadAsync(buffer, totalBytesRead, buffer.Length);
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
                        {   // 1:::1:::FullName:::DoB
                            form.Invoke(() => {
                            form.Hide(); form.ClearPasswordField();
                                new LoggedInForm(fields[2], fields[3],username,form).Show(); });                                                   
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
