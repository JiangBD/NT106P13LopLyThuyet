namespace ClientSide
{
    partial class SearchBooksUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbSearch = new TextBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            progressBar1 = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tbSearch
            // 
            tbSearch.Font = new Font("Segoe UI", 12F);
            tbSearch.Location = new Point(0, 0);
            tbSearch.Name = "tbSearch";
            tbSearch.PlaceholderText = "Nhập Thông Tin Về Sách Cần Tìm ...";
            tbSearch.Size = new Size(554, 39);
            tbSearch.TabIndex = 0;
            tbSearch.KeyDown += tbSearch_KeyDown;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.lenseicon;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(550, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(46, 39);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Location = new Point(0, 45);
            panel1.Name = "panel1";
            panel1.Size = new Size(884, 527);
            panel1.TabIndex = 2;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.BackColor = Color.Green;
            progressBar1.Location = new Point(3, 570);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(881, 34);
            progressBar1.TabIndex = 3;
            // 
            // SearchBooksUserControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(progressBar1);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(tbSearch);
            Name = "SearchBooksUserControl";
            Size = new Size(884, 607);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbSearch;
        private PictureBox pictureBox1;
        private Panel panel1;
        private ProgressBar progressBar1;
    }
}
