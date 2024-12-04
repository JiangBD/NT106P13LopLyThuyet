namespace ClientSide
{
    partial class ChangePasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbPswd = new TextBox();
            tbConfirmPswd = new TextBox();
            btChangePassword = new Button();
            SuspendLayout();
            // 
            // tbPswd
            // 
            tbPswd.Font = new Font("Segoe UI", 11F);
            tbPswd.Location = new Point(93, 44);
            tbPswd.MaxLength = 16;
            tbPswd.Name = "tbPswd";
            tbPswd.PasswordChar = '*';
            tbPswd.PlaceholderText = "Mật Khẩu Mới";
            tbPswd.Size = new Size(236, 37);
            tbPswd.TabIndex = 0;
            // 
            // tbConfirmPswd
            // 
            tbConfirmPswd.Font = new Font("Segoe UI", 11F);
            tbConfirmPswd.Location = new Point(93, 87);
            tbConfirmPswd.Name = "tbConfirmPswd";
            tbConfirmPswd.PasswordChar = '*';
            tbConfirmPswd.PlaceholderText = "Xác Nhận Mật Khẩu";
            tbConfirmPswd.Size = new Size(236, 37);
            tbConfirmPswd.TabIndex = 1;
            // 
            // btChangePassword
            // 
            btChangePassword.Font = new Font("Segoe UI", 11F);
            btChangePassword.Location = new Point(93, 130);
            btChangePassword.Name = "btChangePassword";
            btChangePassword.Size = new Size(236, 34);
            btChangePassword.TabIndex = 2;
            btChangePassword.Text = "Đổi Mật Khẩu";
            btChangePassword.UseVisualStyleBackColor = true;
            btChangePassword.Click += btChangePassword_Click;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background2;
            ClientSize = new Size(435, 270);
            Controls.Add(btChangePassword);
            Controls.Add(tbConfirmPswd);
            Controls.Add(tbPswd);
            Name = "ChangePasswordForm";
            Text = "Đổi Mật Khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbPswd;
        private TextBox tbConfirmPswd;
        private Button btChangePassword;
    }
}