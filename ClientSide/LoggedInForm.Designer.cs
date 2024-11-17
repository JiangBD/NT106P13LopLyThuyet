namespace ClientSide
{
    partial class LoggedInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggedInForm));
            panel1 = new Panel();
            btLogout = new Button();
            btMyShelves = new Button();
            btSearchBooks = new Button();
            lbUserInfo = new Label();
            pbUserIcon = new PictureBox();
            pnContent = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbUserIcon).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Bisque;
            panel1.Controls.Add(btLogout);
            panel1.Controls.Add(btMyShelves);
            panel1.Controls.Add(btSearchBooks);
            panel1.Controls.Add(lbUserInfo);
            panel1.Controls.Add(pbUserIcon);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(238, 581);
            panel1.TabIndex = 0;
            // 
            // btLogout
            // 
            btLogout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btLogout.BackgroundImageLayout = ImageLayout.None;
            btLogout.Image = Properties.Resources.logout222221;
            btLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btLogout.Location = new Point(0, 289);
            btLogout.Name = "btLogout";
            btLogout.Size = new Size(238, 47);
            btLogout.TabIndex = 4;
            btLogout.Text = "   Đăng Xuất";
            btLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            btLogout.UseVisualStyleBackColor = true;
            btLogout.Click += btLogout_Click_1;
            // 
            // btMyShelves
            // 
            btMyShelves.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btMyShelves.Location = new Point(0, 245);
            btMyShelves.Name = "btMyShelves";
            btMyShelves.Size = new Size(238, 47);
            btMyShelves.TabIndex = 3;
            btMyShelves.Text = "Kệ Sách Của Tôi";
            btMyShelves.UseVisualStyleBackColor = true;
            btMyShelves.Click += btMyShelves_Click;
            // 
            // btSearchBooks
            // 
            btSearchBooks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btSearchBooks.Location = new Point(0, 202);
            btSearchBooks.Name = "btSearchBooks";
            btSearchBooks.Size = new Size(238, 47);
            btSearchBooks.TabIndex = 2;
            btSearchBooks.Text = "Tìm Kiếm Sách";
            btSearchBooks.UseVisualStyleBackColor = true;
            btSearchBooks.Click += btSearchBooks_Click;
            // 
            // lbUserInfo
            // 
            lbUserInfo.AutoSize = true;
            lbUserInfo.Font = new Font("Segoe UI", 12F);
            lbUserInfo.Location = new Point(3, 140);
            lbUserInfo.Name = "lbUserInfo";
            lbUserInfo.Size = new Size(0, 32);
            lbUserInfo.TabIndex = 1;
            // 
            // pbUserIcon
            // 
            pbUserIcon.BackgroundImage = (Image)resources.GetObject("pbUserIcon.BackgroundImage");
            pbUserIcon.BackgroundImageLayout = ImageLayout.Stretch;
            pbUserIcon.Dock = DockStyle.Top;
            pbUserIcon.Location = new Point(0, 0);
            pbUserIcon.Name = "pbUserIcon";
            pbUserIcon.Size = new Size(238, 137);
            pbUserIcon.TabIndex = 0;
            pbUserIcon.TabStop = false;
            // 
            // pnContent
            // 
            pnContent.BackColor = Color.LemonChiffon;
            pnContent.Dock = DockStyle.Fill;
            pnContent.Location = new Point(238, 0);
            pnContent.Name = "pnContent";
            pnContent.Size = new Size(774, 581);
            pnContent.TabIndex = 1;
            // 
            // LoggedInForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1012, 581);
            Controls.Add(pnContent);
            Controls.Add(panel1);
            Name = "LoggedInForm";
            Text = "MyApp";
            FormClosed += LoggedInForm_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbUserIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pbUserIcon;
        private Panel pnContent;
        private Label lbUserInfo;
        private Button btSearchBooks;
        private Button btMyShelves;
        private Button btLogout;
    }
}