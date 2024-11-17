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
    public partial class AllMyShelvesUserControl : UserControl
    {
        public AllMyShelvesUserControl()
        {
            InitializeComponent();
            LoadMyBookshelves();
        }
        private async void LoadMyBookshelves()
        {
            List<Bookshelf> shelves = CurrentGoogleBooksUser.User.Bookshelves;
            for(int i = shelves.Count - 1; i > -1  ; i--)
            {
                MyBookShelfUserControl x = new MyBookShelfUserControl(shelves[i]);
                x.Dock = DockStyle.Top;
                panel1.Controls.Add(x);
            }      
        }
    }
}
