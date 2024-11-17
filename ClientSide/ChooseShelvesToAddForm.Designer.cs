namespace ClientSide
{
    partial class ChooseShelvesToAddForm
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
            checkedListBox1 = new CheckedListBox();
            label1 = new Label();
            btAdd = new Button();
            SuspendLayout();
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(12, 57);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(360, 256);
            checkedListBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(0, 25);
            label1.TabIndex = 1;
            // 
            // btAdd
            // 
            btAdd.Font = new Font("Segoe UI", 11F);
            btAdd.Location = new Point(111, 319);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(140, 41);
            btAdd.TabIndex = 2;
            btAdd.Text = "Thêm Sách";
            btAdd.UseVisualStyleBackColor = true;
            btAdd.Click += btAdd_Click;
            // 
            // ChooseShelvesToAddForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 364);
            Controls.Add(btAdd);
            Controls.Add(label1);
            Controls.Add(checkedListBox1);
            Name = "ChooseShelvesToAddForm";
            Text = "Thêm Sách Vào Kệ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox checkedListBox1;
        private Label label1;
        private Button btAdd;
    }
}