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
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();

        }
        private bool HasUnicodeCharacter(string s)
        {
            byte[] array = Encoding.UTF8.GetBytes(s);
            if (s.Length != array.Length) return true;
            else return false;
        }
        private bool HasSpecialCharacter(string s)
        {
            foreach (char c in s)
                if (!Char.IsLetterOrDigit(c)) return true;
            return false;
        }
        private bool HasEmptyField()
        {
            if
          (tbPswd.Text == null || tbPswd.Text == ""
          || tbConfirmPswd.Text == null || tbConfirmPswd.Text == ""
           ) return true;
            else return false;
        }
        private void CheckAndSubmitNewPassword()
        {

            if (this.HasEmptyField()) { this.Invoke(() => MessageBox.Show("Không được để trống các trường")); return; }
            if (this.tbPswd.Text.Length < 8) { this.Invoke(() => MessageBox.Show("Mật khẩu phải có tối thiểu 8 kí tự!")); return; }
            if (this.HasUnicodeCharacter(tbPswd.Text))
            {
                this.Invoke(() => MessageBox.Show("Mật khẩu không được bao gồm kí tự Unicode!" +
            "\n(Có thể thử tắt hoặc chuyển tạm thời Unikey sang tiếng Anh (E))")); return;
            }

            if (!tbPswd.Text.Equals(tbConfirmPswd.Text))
            { this.Invoke(() => MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!")); return; }
            if (!this.HasSpecialCharacter(tbPswd.Text))
            { this.Invoke(() => MessageBox.Show("Mật khẩu phải có ít nhất 1 kí tự đặc biệt!")); return; }
            if (this.HasEmptyField()) { this.Invoke(() => MessageBox.Show("Không được để trống các trường")); return; }


             Station.SendChangePasswordRequest(Station.GetCurrentUsername(),tbPswd.Text,  this);


        }

        private void btChangePassword_Click(object sender, EventArgs e)
        {
            CheckAndSubmitNewPassword();
        }
    }
}
