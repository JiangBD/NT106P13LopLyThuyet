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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientSide
{
    public partial class BrowseBookshelfForm : Form
    {
        private Bookshelf shelf;
        public BrowseBookshelfForm()
        {
            InitializeComponent();
        }
        public BrowseBookshelfForm(Bookshelf s)
        {
            shelf = s;
            InitializeComponent();
            lbBookshelfTitle.Text = s.Title;
            pBar.Hide();
            LoadBookUserControls();
        }
        private async void LoadBookUserControls()
        {            
            pBar.Show();
            List<BookUserControl> bookUserControls = await GoogleBooksWorker
                .GetBookUserControls(CurrentGoogleBooksUser.User.Credential, this.shelf, this);
            for (int i = bookUserControls.Count - 1; i >= 0; i--)
            {
                bookUserControls[i].Dock = DockStyle.Top;

                panel1.Controls.Add(bookUserControls[i]);
                                
                bookUserControls[i].HideAddButton();
            }
            pBar.Value = 0;
            pBar.Hide();     
        }


        public void UpdateSearchProgress(int prog)
        {
            Invoke(() => pBar.Value = prog);
        }
    }
}
