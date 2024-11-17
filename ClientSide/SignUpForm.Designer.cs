namespace ClientSide
{
    partial class SignUpForm
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
            label1 = new Label();
            tbNewUserName = new TextBox();
            tbPswd = new TextBox();
            tbConfirmPswd = new TextBox();
            btnSignUp = new Button();
            tbEmail = new TextBox();
            tbFullName = new TextBox();
            monthCalendar = new MonthCalendar();
            lbDoB = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Georgia", 17F);
            label1.Location = new Point(208, 26);
            label1.Name = "label1";
            label1.Size = new Size(364, 39);
            label1.TabIndex = 0;
            label1.Text = "TẠO TÀI KHOẢN MỚI";
            // 
            // tbNewUserName
            // 
            tbNewUserName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbNewUserName.Font = new Font("Segoe UI", 11F);
            tbNewUserName.Location = new Point(208, 80);
            tbNewUserName.Name = "tbNewUserName";
            tbNewUserName.PlaceholderText = "Tên đăng nhập";
            tbNewUserName.Size = new Size(336, 37);
            tbNewUserName.TabIndex = 1;
            // 
            // tbPswd
            // 
            tbPswd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPswd.Font = new Font("Segoe UI", 11F);
            tbPswd.Location = new Point(208, 123);
            tbPswd.Name = "tbPswd";
            tbPswd.PasswordChar = '*';
            tbPswd.PlaceholderText = "Mật khẩu";
            tbPswd.Size = new Size(336, 37);
            tbPswd.TabIndex = 2;
            // 
            // tbConfirmPswd
            // 
            tbConfirmPswd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbConfirmPswd.Font = new Font("Segoe UI", 11F);
            tbConfirmPswd.Location = new Point(208, 166);
            tbConfirmPswd.Name = "tbConfirmPswd";
            tbConfirmPswd.PasswordChar = '*';
            tbConfirmPswd.PlaceholderText = "Xác nhận mật khẩu";
            tbConfirmPswd.Size = new Size(336, 37);
            tbConfirmPswd.TabIndex = 3;
            // 
            // btnSignUp
            // 
            btnSignUp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSignUp.BackColor = Color.LightGreen;
            btnSignUp.Font = new Font("Segoe UI", 11F);
            btnSignUp.ForeColor = Color.DarkSlateGray;
            btnSignUp.Location = new Point(208, 628);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(336, 43);
            btnSignUp.TabIndex = 5;
            btnSignUp.Text = "ĐĂNG KÍ";
            btnSignUp.UseVisualStyleBackColor = false;
            btnSignUp.Click += btnSignUp_Click;
            // 
            // tbEmail
            // 
            tbEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbEmail.Font = new Font("Segoe UI", 11F);
            tbEmail.Location = new Point(208, 252);
            tbEmail.Name = "tbEmail";
            tbEmail.PlaceholderText = "Email";
            tbEmail.Size = new Size(336, 37);
            tbEmail.TabIndex = 4;
            // 
            // tbFullName
            // 
            tbFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbFullName.Font = new Font("Segoe UI", 11F);
            tbFullName.Location = new Point(208, 209);
            tbFullName.Name = "tbFullName";
            tbFullName.PlaceholderText = "Họ và Tên";
            tbFullName.Size = new Size(336, 37);
            tbFullName.TabIndex = 6;
            // 
            // monthCalendar
            // 
            monthCalendar.Location = new Point(208, 326);
            monthCalendar.MinDate = new DateTime(1980, 1, 1, 0, 0, 0, 0);
            monthCalendar.Name = "monthCalendar";
            monthCalendar.ShowToday = false;
            monthCalendar.TabIndex = 7;
            monthCalendar.DateSelected += monthCalendar_DateSelected;
            // 
            // lbDoB
            // 
            lbDoB.AutoSize = true;
            lbDoB.BackColor = SystemColors.Window;
            lbDoB.Location = new Point(208, 590);
            lbDoB.Name = "lbDoB";
            lbDoB.Size = new Size(214, 25);
            lbDoB.TabIndex = 8;
            lbDoB.Text = "Bạn chưa chọn ngày sinh!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Window;
            label2.Location = new Point(208, 292);
            label2.Name = "label2";
            label2.Size = new Size(139, 25);
            label2.TabIndex = 9;
            label2.Text = "Chọn ngày sinh:";
            // 
            // SignUpForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            BackgroundImage = ClientSide.Properties.Resources.background2;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(902, 706);
            Controls.Add(label2);
            Controls.Add(lbDoB);
            Controls.Add(monthCalendar);
            Controls.Add(tbFullName);
            Controls.Add(tbEmail);
            Controls.Add(btnSignUp);
            Controls.Add(tbConfirmPswd);
            Controls.Add(tbPswd);
            Controls.Add(tbNewUserName);
            Controls.Add(label1);
            DoubleBuffered = true;
            ForeColor = SystemColors.ControlText;
            Name = "SignUpForm";
            Text = "MyApp";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbNewUserName;
        private TextBox tbPswd;
        private TextBox tbConfirmPswd;
        private Button btnSignUp;
        private TextBox tbEmail;
        private TextBox tbFullName;
        private MonthCalendar monthCalendar;
        private Label lbDoB;
        private Label label2;
    }
}