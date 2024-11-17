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
    public partial class SearchBooksUserControl : UserControl
    {
        public SearchBooksUserControl()
        {
            InitializeComponent();
            progressBar1.Hide();
        }

        private async void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && GoogleBooksWorker.IsConnectedToTheInternet())
            {  
                    string searchStr = tbSearch.Text;
                    if (!string.IsNullOrEmpty(searchStr))
                    {
                        tbSearch.Enabled = false;
                        progressBar1.Show();
                        panel1.Controls.Clear();
                        List<BookUserControl> bookUserControls = await GoogleBooksWorker.GetBookUserControls(searchStr, this);
                        for (int i = bookUserControls.Count - 1; i >= 0; i--)
                        {
                            bookUserControls[i].Dock = DockStyle.Top;

                            panel1.Controls.Add(bookUserControls[i]);
                            bookUserControls[i].HideRemoveButton();
                        }

                        progressBar1.Value = 0;
                        progressBar1.Hide();
                        tbSearch.Enabled = true;
                    }
                      
            }
        }
        public void UpdateSearchProgress(int prog)
        {
            Invoke(() => progressBar1.Value = prog);
        }
    }
}
