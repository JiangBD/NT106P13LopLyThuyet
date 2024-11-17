namespace ClientSide
{
    partial class BookUserControl
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
            panel1 = new Panel();
            lbDescription = new Label();
            btAdd = new Button();
            btRemove = new Button();
            pbBookThumbnail = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbBookThumbnail).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lbDescription);
            panel1.Controls.Add(btAdd);
            panel1.Controls.Add(btRemove);
            panel1.Controls.Add(pbBookThumbnail);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1009, 343);
            panel1.TabIndex = 0;
            // 
            // lbDescription
            // 
            lbDescription.AutoSize = true;
            lbDescription.Font = new Font("Segoe UI", 11F);
            lbDescription.Location = new Point(201, 5);
            lbDescription.Name = "lbDescription";
            lbDescription.Size = new Size(71, 30);
            lbDescription.TabIndex = 7;
            lbDescription.Text = "label1";
            // 
            // btAdd
            // 
            btAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btAdd.Location = new Point(844, 306);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(165, 37);
            btAdd.TabIndex = 6;
            btAdd.Text = "Thêm Vào Kệ Sách";
            btAdd.UseVisualStyleBackColor = true;
            btAdd.Click += btAdd_Click;
            // 
            // btRemove
            // 
            btRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRemove.Location = new Point(844, -1);
            btRemove.Name = "btRemove";
            btRemove.Size = new Size(165, 37);
            btRemove.TabIndex = 5;
            btRemove.Text = "Loại Khỏi Kệ Sách";
            btRemove.UseVisualStyleBackColor = true;
            btRemove.Click += btRemove_Click;
            // 
            // pbBookThumbnail
            // 
            pbBookThumbnail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pbBookThumbnail.Location = new Point(0, 3);
            pbBookThumbnail.Name = "pbBookThumbnail";
            pbBookThumbnail.Size = new Size(195, 340);
            pbBookThumbnail.TabIndex = 4;
            pbBookThumbnail.TabStop = false;
            // 
            // BookUserControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PapayaWhip;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(panel1);
            Name = "BookUserControl";
            Size = new Size(1009, 343);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbBookThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btAdd;
        private Button btRemove;
        private PictureBox pbBookThumbnail;
        private Label lbDescription;
    }
}
