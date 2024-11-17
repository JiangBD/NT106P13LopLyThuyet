namespace ClientSide
{
    partial class BrowseBookshelfForm
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
            lbBookshelfTitle = new Label();
            panel1 = new Panel();
            pBar = new ProgressBar();
            SuspendLayout();
            // 
            // lbBookshelfTitle
            // 
            lbBookshelfTitle.AutoSize = true;
            lbBookshelfTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lbBookshelfTitle.Location = new Point(12, 9);
            lbBookshelfTitle.Name = "lbBookshelfTitle";
            lbBookshelfTitle.Size = new Size(83, 32);
            lbBookshelfTitle.TabIndex = 0;
            lbBookshelfTitle.Text = "label1";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Location = new Point(2, 44);
            panel1.Name = "panel1";
            panel1.Size = new Size(798, 371);
            panel1.TabIndex = 1;
            // 
            // pBar
            // 
            pBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pBar.Location = new Point(2, 415);
            pBar.Name = "pBar";
            pBar.Size = new Size(798, 34);
            pBar.TabIndex = 2;
            // 
            // BrowseBookshelfForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pBar);
            Controls.Add(panel1);
            Controls.Add(lbBookshelfTitle);
            Name = "BrowseBookshelfForm";
            Text = "Kệ Sách Của Tôi";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbBookshelfTitle;
        private Panel panel1;
        private ProgressBar pBar;
    }
}