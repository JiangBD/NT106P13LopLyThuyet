using Google.Apis.Books.v1.Data;
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
    public partial class BookUserControl : UserControl
    {
        public GoogleBookInfo BookInfo { get; set; }
        private Bookshelf shelf;
        public BookUserControl()
        {
            InitializeComponent();
        }
        public BookUserControl(GoogleBookInfo bookInfo, Image im)
        {/* Id = id; Title = title; Authors = authors;
                PublishedDate = publishedDate; Description = des; PageCount = count; Thumbnail = link;*/
            InitializeComponent();
            BookInfo = bookInfo;
            string infoStr = bookInfo.Title + "\n" + "Tác giả: ";
            foreach (var author in bookInfo.Authors)
                infoStr += author + ", ";
            infoStr.Remove(infoStr.Length - 2);
            infoStr += "  Ngày xuất bản: " + bookInfo.PublishedDate + "\n";
            infoStr += $"Số trang: {bookInfo.PageCount}\n";
            string des = bookInfo.Description;
            if (!des.Equals("N/A")) des = des.Insert(des.Length / 2, "\n");
            infoStr += "Sơ lược: " + des + "...";
            if (im != null)
            { pbBookThumbnail.BackgroundImage = im; pbBookThumbnail.BackgroundImageLayout = ImageLayout.Stretch; }
            lbDescription.Text = infoStr;
        }
        public BookUserControl(Bookshelf sh, GoogleBookInfo bookInfo, Image im)
        {/* Id = id; Title = title; Authors = authors;
                PublishedDate = publishedDate; Description = des; PageCount = count; Thumbnail = link;*/
            shelf = sh;
            InitializeComponent();
            BookInfo = bookInfo;
            string infoStr = bookInfo.Title + "\n" + "Tác giả: ";
            foreach (var author in bookInfo.Authors)
                infoStr += author + ", ";
            infoStr.Remove(infoStr.Length - 2);
            infoStr += "  Ngày xuất bản: " + bookInfo.PublishedDate + "\n";
            infoStr += $"Số trang: {bookInfo.PageCount}\n";
            string des = bookInfo.Description;
            if (!des.Equals("N/A")) des = des.Insert(des.Length / 2, "\n");
            infoStr += "Sơ lược: " + des + "...";
            if (im != null)
            { pbBookThumbnail.BackgroundImage = im; pbBookThumbnail.BackgroundImageLayout = ImageLayout.Stretch; }
            lbDescription.Text = infoStr;
        }
        public void HideRemoveButton()
        {
            Invoke(() => btRemove.Hide());
        }
        public void HideAddButton()
        {
            Invoke(() => btAdd.Hide());
        }
        public void DisableAddButton()
        {
            Invoke(() => btAdd.Enabled = false);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (GoogleBooksWorker.IsConnectedToTheInternet()
                && CurrentGoogleBooksUser.User.Credential != null && CurrentGoogleBooksUser.User.Bookshelves != null)
            {
                (new ChooseShelvesToAddForm(BookInfo, this)).ShowDialog();
            }
        }

        private async void btRemove_Click(object sender, EventArgs e)
        {
            if (GoogleBooksWorker.IsConnectedToTheInternet()
                && CurrentGoogleBooksUser.User.Credential != null && CurrentGoogleBooksUser.User.Bookshelves != null)
            {
                Panel panel = (Panel)this.Parent;
                panel.Enabled = false;
                await GoogleBooksWorker
                    .RemoveBookFromShelf(CurrentGoogleBooksUser.User.Credential.Token.AccessToken, shelf.Id.ToString(), BookInfo.Id);
                
                panel.Controls.Remove(this);
                panel.Enabled = true;
                MessageBox.Show("Đã xóa khỏi kệ!");
            }
        }
    }
}
