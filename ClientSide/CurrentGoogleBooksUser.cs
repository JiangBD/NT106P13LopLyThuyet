using Google.Apis.Auth.OAuth2;
using Google.Apis.Books.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide
{
    internal class CurrentGoogleBooksUser
    {
        public static CurrentGoogleBooksUser User;
        public UserCredential Credential {  get; set; }
        public List<Bookshelf> Bookshelves { get; set; }
        private CurrentGoogleBooksUser() { }
        public static void CreateCurrentGoogleBooksUser()
        {User = new CurrentGoogleBooksUser();}
        public static async void InvalidateCurrentGoogleBooksUser()
        {
            if (User != null)
            {
                if (User.Credential != null) await User.Credential.RevokeTokenAsync(CancellationToken.None);
                
                User = null;
            }

        }
    }
}
