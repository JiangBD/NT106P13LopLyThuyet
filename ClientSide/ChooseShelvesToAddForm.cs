using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    public partial class ChooseShelvesToAddForm : Form
    {   private GoogleBookInfo bookInfo;
        private BookUserControl bookUserControl;
        public ChooseShelvesToAddForm()
        {
            InitializeComponent();
        }
        public ChooseShelvesToAddForm(GoogleBookInfo info, BookUserControl uc)
        {
            InitializeComponent();
            this.bookInfo = info;
            bookUserControl = uc;
            label1.Text = "Chọn kệ sách để thêm " + bookInfo.Title + " vào:";
            foreach (var x in CurrentGoogleBooksUser.User.Bookshelves)
                checkedListBox1.Items.Add(x.Title);
        }

        private async void btAdd_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {   foreach (var x in checkedListBox1.CheckedItems)
                {
                    string shelfTitle = x.ToString();
                    string shelfId = "";
                    foreach (var y in CurrentGoogleBooksUser.User.Bookshelves)
                        if (y.Title.Equals(shelfTitle)) { shelfId = y.Id.ToString(); break; }
                    if (shelfId != null)
                        await GoogleBooksWorker.AddBookToShelf(CurrentGoogleBooksUser.User.Credential.Token.AccessToken, shelfId,bookInfo.Id);
                }
                MessageBox.Show("Đã thêm sách vào kệ!");




                bookUserControl.DisableAddButton();
                this.Close();
            }

        }
    }
}
