using Google.Apis.Books.v1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    public partial class MyBookShelfUserControl : UserControl
    {
        private Bookshelf shelf;
        public MyBookShelfUserControl()
        {
            InitializeComponent();
        }
        public MyBookShelfUserControl(Bookshelf s)
        {
            shelf = s;
            InitializeComponent();
            label1.Text = shelf.Title;
        }

        private void MyBookShelfUserControl_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {   if (GoogleBooksWorker.IsConnectedToTheInternet())
            (new BrowseBookshelfForm(shelf)).ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (GoogleBooksWorker.IsConnectedToTheInternet())
                (new BrowseBookshelfForm(shelf)).ShowDialog();
        }
    }
}
