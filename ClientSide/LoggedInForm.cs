using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    public partial class LoggedInForm : Form
    {

        private LoginForm lf;
        private string UserName;
        public LoggedInForm(string fullname, string dob, string username, LoginForm l)
        {
            lf = l;
            UserName = username;
            InitializeComponent();
            lbUserInfo.Text = fullname + "\n" + dob.Replace(" ", @"/");
            CurrentGoogleBooksUser.CreateCurrentGoogleBooksUser();
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
          if(UserName != "11")  Station.SendLogoutRequest(UserName);
        }

        private void LoggedInForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CurrentGoogleBooksUser.InvalidateCurrentGoogleBooksUser();
            
            lf.Show();
        }

        private void btSearchBooks_Click(object sender, EventArgs e)
        {
            if (GoogleBooksWorker.IsConnectedToTheInternet())
            {
                pnContent.Controls.Clear();
                var x = new SearchBooksUserControl();
                x.Dock = DockStyle.Fill;
                pnContent.Controls.Add(x);
                x.Show();
            }
        }

        private async void btMyShelves_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            if (GoogleBooksWorker.IsConnectedToTheInternet())
            {
                
                if (CurrentGoogleBooksUser.User.Credential == null)
                {
                    Task.Run(async () =>
                    {
                        CurrentGoogleBooksUser.User.Credential = await GoogleBooksWorker.GetUserCredentialAsync();
                        CurrentGoogleBooksUser.User.Bookshelves =
                        await GoogleBooksWorker.GetMyGoogleBookshelves(CurrentGoogleBooksUser.User.Credential);

                    });
                    MessageBox.Show("Người dùng Google chưa cấp quyền truy cập kệ sách");

                }
                else
                {
                    if (CurrentGoogleBooksUser.User.Bookshelves != null)
                    {
                        pnContent.Controls.Clear();
                        var x = new AllMyShelvesUserControl();
                        x.Dock = DockStyle.Fill;
                        pnContent.Controls.Add(x);
                    }
                }
            }
            panel1.Enabled = true;
        }

        private void btLogout_Click_1(object sender, EventArgs e)
        {   
            if (!UserName.Equals("11"))            
            Station.SendLogoutRequest(UserName);

            this.Close();
            
        }
    }
}
