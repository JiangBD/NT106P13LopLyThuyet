using ClientSide;
using Microsoft.VisualBasic;
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
    public partial class SignUpForm : Form
    {
        private delegate void CheckValidityDialogDelegate();
        private delegate int TestDelegate();
        private bool HasSelectedDoB;
        private CheckValidityDialogDelegate
            PasswordTooShortDelegate, NoSpecialCharacterDelegate, HasUnicodeCharacterDelegate
            , PasswordFieldsMismatchDelegate, EmptyFieldDelegate, SignUpSuccessDelegate, ExistsUserNameDelegate;
        public SignUpForm()
        {
            HasSelectedDoB = false;
            InitializeComponent();
            
            PasswordTooShortDelegate = new CheckValidityDialogDelegate(ShowPasswordTooShortDialog);
            NoSpecialCharacterDelegate = new CheckValidityDialogDelegate(ShowNoSpecialCharacterDialog);
            HasUnicodeCharacterDelegate = new CheckValidityDialogDelegate(ShowUnicodeCharacterDialog);
            PasswordFieldsMismatchDelegate = new CheckValidityDialogDelegate(ShowPasswordFieldsMismatchDelegate);
            EmptyFieldDelegate = new CheckValidityDialogDelegate(ShowEmptyFieldDialog);
            SignUpSuccessDelegate = new CheckValidityDialogDelegate(ShowSignUpSuccessDialog);
            ExistsUserNameDelegate = new CheckValidityDialogDelegate(ShowExistsUserNameDialog);
        }
        private void ShowExistsUserNameDialog()
        {
            MessageBox.Show("Tên người dùng đã tồn tại");
        }
        private void ShowPasswordFieldsMismatchDelegate()
        {
            MessageBox.Show("Mật khẩu và Xác nhận mật khẩu phải khớp nhau!");
        }
        private void ShowEmptyFieldDialog()
        {
            MessageBox.Show("Không được để trống các trường");
        }
        private void ShowSignUpSuccessDialog()
        {
            MessageBox.Show("Đăng kí người dùng mới thành công!");
        }
        private void ShowPasswordTooShortDialog()
        {
            MessageBox.Show("Mật khẩu phải có tối thiểu 8 kí tự!");
        }
        private void ShowNoSpecialCharacterDialog()
        {
            MessageBox.Show("Mật khẩu phải có ít nhất 1 kí tự đặc biệt!");
        }
        private void ShowUnicodeCharacterDialog()
        {
            MessageBox.Show("Tên người dùng hoặc mật khẩu không được bao gồm kí tự Unicode!" +
                "\n(Có thể thử tắt hoặc chuyển tạm thời Unikey sang tiếng Anh (E))");
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
          (tbNewUserName.Text == null || tbNewUserName.Text == "" || tbPswd.Text == null || tbPswd.Text == ""
          || tbConfirmPswd.Text == null || tbConfirmPswd.Text == ""
          || tbEmail.Text == null || tbEmail.Text == ""
          || !HasSelectedDoB || tbFullName.Text == "") return true;
            else return false;
        }
        private bool IsValidEmail()
        {   string email = tbEmail.Text;
            return (email.Contains('@') && email.Contains('.'));        
        }

        private void CheckAndSubmitNewUser()
        {
            
           
           Task.Run(() =>
              {
                  if (this.HasEmptyField()) { this.Invoke(() => ShowEmptyFieldDialog()); return; }
                  if (this.tbPswd.Text.Length < 8) { this.Invoke(PasswordTooShortDelegate); return; }
                  if (this.HasUnicodeCharacter(tbNewUserName.Text) || this.HasUnicodeCharacter(tbPswd.Text))
                  { this.Invoke(HasUnicodeCharacterDelegate); return; }
                  if (!tbPswd.Text.Equals(tbConfirmPswd.Text))
                  { this.Invoke(() => { MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!"); return; });     }
                  if (!this.HasSpecialCharacter(tbPswd.Text)) { this.Invoke(NoSpecialCharacterDelegate); return; }
                  if (this.HasEmptyField()) { this.Invoke(EmptyFieldDelegate); return; }
                  if (!IsValidEmail())
                  { this.Invoke(() => MessageBox.Show("Địa chỉ email không hợp lệ!")); return;  }
                  User u = new User(tbNewUserName.Text, 
                      tbPswd.Text,tbFullName.Text, tbEmail.Text, monthCalendar.SelectionStart);

                  Station.SendRegistrationRequest(u, this);
              });
           
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {                
            
            CheckAndSubmitNewUser();            
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            HasSelectedDoB = true;
            DateTime dob = monthCalendar.SelectionStart;
            lbDoB.Text = dob.Day.ToString() + "/" + dob.Month.ToString() + "/" + dob.Year.ToString();
        }
    }
}
