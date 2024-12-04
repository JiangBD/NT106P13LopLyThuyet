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
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            string username = tbUserName.Text;
            string emailAddress = tbEmail.Text;
            if (!string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(username) ) 
                Station.SendForgotPasswordRequest(username, emailAddress,this);
            Thread.Sleep(120);
        }
    }
}
