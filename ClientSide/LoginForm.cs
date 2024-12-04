using ClientSide;
using System.Text;

namespace ClientSide
{
    public partial class LoginForm : Form
    {
        private delegate void LoginResultDialogDelegate();
        private LoginResultDialogDelegate LoginSuccessDelegate, LoginFailureDelegate;
        public LoginForm()
        {
            InitializeComponent();
            LoginSuccessDelegate = new LoginResultDialogDelegate(ShowLoginSuccessDialog);
            LoginFailureDelegate = new LoginResultDialogDelegate(ShowLoginFailureDialog);

        }
        public void ClearPasswordField()
        {

            tbPassword.Clear();
            tbUserName.Clear();
        }
        private void ShowLoginSuccessDialog()
        {

            MessageBox.Show("Đăng nhập thành công!");
        }
        private void ShowLoginFailureDialog()
        {

            MessageBox.Show("Đăng nhập không thành công.\nVui lòng thử lại.");
        }
        private void linkLbSignUp_Click(object sender, EventArgs e)
        {

            new SignUpForm().ShowDialog();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /// CLOSE THE SINGLETON ACCESSOR
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text.Equals("11")) // CHEAT, for testing only
            { Station.SendLoginRequest(tbUserName.Text, "rubbish", this); return; }

            string username = tbUserName.Text;
            string pswd = tbPassword.Text;
            if (username == "" || pswd == "" || username == null || pswd == null)
            {
                MessageBox.Show("Không được để trống các trường!");
                return;
            }
            Station.SendLoginRequest(username, pswd, this);
        }
        public void ShowWrongPasswordDialog()
        {
            MessageBox.Show("Mật khẩu không đúng!");
        }
        public void ShowWrongNonUserNameDialog()
        {
            MessageBox.Show("Tên tài khoản không tồn tại!");
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLbForgetPassword_Click(object sender, EventArgs e)
        {
            new ForgotPasswordForm().ShowDialog();
        }
    }
}
