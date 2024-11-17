namespace ClientSide
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            label1 = new Label();
            tbUserName = new TextBox();
            tbPassword = new TextBox();
            btnLogin = new Button();
            linkLbSignUp = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Georgia", 17F);
            label1.Location = new Point(295, 80);
            label1.Name = "label1";
            label1.Size = new Size(222, 39);
            label1.TabIndex = 0;
            label1.Text = "ĐĂNG NHẬP";
            // 
            // tbUserName
            // 
            tbUserName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbUserName.Font = new Font("Segoe UI", 11F);
            tbUserName.Location = new Point(295, 174);
            tbUserName.Name = "tbUserName";
            tbUserName.PlaceholderText = "Tên đăng nhập";
            tbUserName.Size = new Size(212, 37);
            tbUserName.TabIndex = 3;
            // 
            // tbPassword
            // 
            tbPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPassword.Font = new Font("Segoe UI", 11F);
            tbPassword.Location = new Point(295, 233);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.PlaceholderText = "Mật khẩu";
            tbPassword.Size = new Size(212, 37);
            tbPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.BackColor = Color.Bisque;
            btnLogin.Font = new Font("Segoe UI", 11F);
            btnLogin.Location = new Point(295, 282);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(212, 38);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // linkLbSignUp
            // 
            linkLbSignUp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linkLbSignUp.AutoSize = true;
            linkLbSignUp.Location = new Point(451, 338);
            linkLbSignUp.MaximumSize = new Size(120, 35);
            linkLbSignUp.Name = "linkLbSignUp";
            linkLbSignUp.Size = new Size(73, 25);
            linkLbSignUp.TabIndex = 6;
            linkLbSignUp.TabStop = true;
            linkLbSignUp.Text = "Đăng kí";
            linkLbSignUp.Click += linkLbSignUp_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(829, 450);
            Controls.Add(linkLbSignUp);
            Controls.Add(btnLogin);
            Controls.Add(tbPassword);
            Controls.Add(tbUserName);
            Controls.Add(label1);
            DoubleBuffered = true;
            Name = "LoginForm";
            Text = "My App";
            FormClosing += LoginForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbUserName;
        private TextBox tbPassword;
        private Button btnLogin;
        private LinkLabel linkLbSignUp;
    }
}
