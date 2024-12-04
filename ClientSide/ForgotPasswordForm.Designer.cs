namespace ClientSide
{
    partial class ForgotPasswordForm
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
            tbUserName = new TextBox();
            tbEmail = new TextBox();
            btSend = new Button();
            SuspendLayout();
            // 
            // tbUserName
            // 
            tbUserName.Font = new Font("Segoe UI", 11F);
            tbUserName.Location = new Point(138, 55);
            tbUserName.Name = "tbUserName";
            tbUserName.PlaceholderText = "Tên Đăng Nhập";
            tbUserName.Size = new Size(206, 37);
            tbUserName.TabIndex = 0;
            // 
            // tbEmail
            // 
            tbEmail.Font = new Font("Segoe UI", 11F);
            tbEmail.Location = new Point(138, 98);
            tbEmail.Name = "tbEmail";
            tbEmail.PlaceholderText = "Địa Chỉ Email";
            tbEmail.Size = new Size(206, 37);
            tbEmail.TabIndex = 1;
            // 
            // btSend
            // 
            btSend.Font = new Font("Segoe UI", 11F);
            btSend.Location = new Point(138, 141);
            btSend.Name = "btSend";
            btSend.Size = new Size(206, 41);
            btSend.TabIndex = 2;
            btSend.Text = "Nhận Email Hỗ Trợ";
            btSend.UseVisualStyleBackColor = true;
            btSend.Click += btSend_Click;
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 283);
            Controls.Add(btSend);
            Controls.Add(tbEmail);
            Controls.Add(tbUserName);
            Name = "ForgotPasswordForm";
            Text = "Quên Mật Khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbUserName;
        private TextBox tbEmail;
        private Button btSend;
    }
}