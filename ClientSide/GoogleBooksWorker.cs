using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json.Serialization;
using static System.Windows.Forms.LinkLabel;
namespace ClientSide
{
    internal class GoogleBooksWorker
    {
        private static async Task<Dictionary<string, GoogleBookInfo>> SearchBookByKeywordAsync(string keyword)
        {
            // Encode the keyword to safely include it in the URL
            string encodedKeyword = HttpUtility.UrlEncode(keyword);
            string apiUrl = $"https://www.googleapis.com/books/v1/volumes?q={encodedKeyword}";
            Dictionary<string, GoogleBookInfo> bookInfos = new();
            try
            {
                using HttpClient client = new HttpClient();
                // Send a GET request to the API
                HttpResponseMessage response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Parse the JSON response
                    Stream s = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    JsonDocument jsonDoc = await JsonDocument.ParseAsync(s).ConfigureAwait(false);
                    // Console.WriteLine(jsonDoc.RootElement.GetRawText());
                    // Navigate through JSON to find book info
                    
                    if (jsonDoc.RootElement.TryGetProperty("items", out JsonElement items) && items.GetArrayLength() > 0)
                    {
                        int totalItems = items.GetArrayLength();
                        
                        // Loop through each book in the results
                        foreach (JsonElement book in items.EnumerateArray())
                        {   string id = book.GetProperty("id").GetString();
                            JsonElement volumeInfo = book.GetProperty("volumeInfo");
                            string title = volumeInfo.GetProperty("title").GetString();
                            List<string> tempList = new();
                            if (volumeInfo.TryGetProperty("authors", out JsonElement authorsElement) )
                              foreach (var author in authorsElement.EnumerateArray())
                                    tempList.Add(author.GetString());
                            string[] authors = tempList.ToArray();
                            tempList.Clear();

                            string publishedDate = volumeInfo.TryGetProperty("publishedDate", out JsonElement publishedDateElement)
                                ? publishedDateElement.GetString() : "N/A";

                            int pageCount = volumeInfo.TryGetProperty("pageCount", out JsonElement pageCountElement)
                                && pageCountElement.TryGetInt32(out int pc)
                                ? pc : 0;

                            string? thumbnail = volumeInfo.TryGetProperty("imageLinks", out JsonElement imageLinksElement)
                                && imageLinksElement.TryGetProperty("thumbnail", out JsonElement thumbnailElement)
                            ? thumbnailElement.GetString() : "N/A";
                            string? description = volumeInfo.TryGetProperty("description", out JsonElement descriptionElement)
                                ? descriptionElement.GetString() : "N/A";
                            description = description.Length < 150 ? description : description.Substring(0, 150);

                            // Display book information
                            // (string id, string title, string[] authors
       //     string publishedDate, string des, int count, string link) 
                            GoogleBookInfo info = new GoogleBookInfo(id,title,authors,publishedDate,description,pageCount,thumbnail);
                            bookInfos.Add(info.Id, info);
                        }
                    }
                    else
                    {
                      //  Console.WriteLine("No books found for this keyword.");
                    }
                }
                else
                {
                  //  Console.WriteLine("Error: Unable to retrieve book information.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message + ex.StackTrace);
            }
            return bookInfos;
        }
        private static async Task<Image> DownloadPosterImage(string url)
        {   if (!url.Contains("http")) url = "https://" + url;
            using HttpClient client = new HttpClient();
            byte[] imageBytes = await client.GetByteArrayAsync(url).ConfigureAwait(false);
            using MemoryStream imageStream = new MemoryStream(imageBytes);
            return Image.FromStream(imageStream);
        }
        public static async Task<List<BookUserControl>> GetBookUserControls(string keyword, SearchBooksUserControl uc)
        {
            Dictionary<string, GoogleBookInfo> bookInfos = await SearchBookByKeywordAsync(keyword);
            int totalItems = bookInfos.Count;
            int done = 0;
            List<BookUserControl> bookUCs = new();
            foreach(var bookInfo in bookInfos.Values)
            {
                Image im;
                if (!bookInfo.Thumbnail.Equals("N/A"))
                    im = await DownloadPosterImage(bookInfo.Thumbnail); //.ConfigureAwait(false)
                else im = null;
                bookUCs.Add(new BookUserControl(bookInfo, im));
                done++;
                uc.UpdateSearchProgress(done * 100 /  totalItems);
            }

            return bookUCs;
        }
        public static async Task<UserCredential> GetUserCredentialAsync()
        {
            UserCredential credential = null;
            try
            {
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    CancellationTokenSource tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(29));

                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { BooksService.Scope.Books },
                        "user",
                         tokenSource.Token, ////  CancellationToken.None
                        new FileDataStore("BookshelfTokenStore")).ConfigureAwait(false);       //"Books.Api.Auth.Store"
                 //   Console.WriteLine("Access Token: " + credential.Token.AccessToken);
                }
            } // end try
            catch (TokenResponseException)
            {
              //  Console.WriteLine("Authorization was canceled, possibly due to the user closing the window.");
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(TokenResponseException))
                {
                //    Console.WriteLine("Authorization failed: access denied.");
                }
            }
            return credential;
        }
        public static async Task AddBookToShelf(string accessToken, string bookshelfId, string volumeId)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set the request URL
                string requestUrl = $"https://www.googleapis.com/books/v1/mylibrary/bookshelves/{bookshelfId}/addVolume?volumeId={volumeId}";

                // Set the authorization header with the OAuth token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                try
                {
                    // Make the POST request
                    HttpResponseMessage response = await client.PostAsync(requestUrl, null).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                       // Console.WriteLine($"Successfully added book '{volumeId}' to bookshelf '{bookshelfId}'.");
                    }
                    else
                    {
                       // Console.WriteLine($"Failed to add book to shelf. Status code: {response.StatusCode}");
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Thêm thất bại, đã có lỗi xảy ra!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm thất bại, đã có lỗi xảy ra!");
                }
            }
        }
        public static async Task RemoveBookFromShelf(string accessToken, string bookshelfId, string volumeId)
        {
            /*string accessToken = "your_access_token";
            string bookshelfId = "your_bookshelf_id"; // e.g., 3 for "Favorites"
            string bookId = "your_book_id";  */         // e.g., "zyTCAlFPjgYC"

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                string url = $"https://www.googleapis.com/books/v1/mylibrary/bookshelves/{bookshelfId}/removeVolume?volumeId={volumeId}";

                HttpResponseMessage response = await client.PostAsync(url, null).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book removed successfully!");
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Console.WriteLine($"Failed to remove book: {response.StatusCode}, {errorContent}");
                }
            }
        }
        public static async Task<List<Bookshelf>> GetMyGoogleBookshelves(UserCredential credential)
        {
            List<Bookshelf> myShelves = new();
            // Assuming 'credential' is already authenticated and valid
            using BooksService booksService = new BooksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "My Google Bookshelf App"
            });
            
            try
            {
                // Step 1: Get all bookshelves
                
                Bookshelves bookshelves = await booksService.Mylibrary.Bookshelves.List().ExecuteAsync().ConfigureAwait(false);

                // Step 2: Find the shelf named "MyUITBookshelf2024"           
               Bookshelf targetShelf = null;
                foreach (var shelf in bookshelves.Items)
                {   var x = shelf.VolumeCount.HasValue ? shelf.VolumeCount : 0;
                    if (x > 0) myShelves.Add(shelf);
                }
                /*  if (targetShelf == null)
                  {
                      Console.WriteLine("Bookshelf 'MyUITBookshelf2024' not found.");
                      return;
                  }
                  Console.WriteLine($"Found bookshelf '{targetShelf.Title}' with ID: {targetShelf.Id}");
                 */
            }
            catch (Exception ex)
            {
               // Console.WriteLine("An error occurred: " + ex.Message);
            } 
            return myShelves;
        }
        public static async Task<List<GoogleBookInfo>> GetMyGoogleBooks(UserCredential credential, Bookshelf targetShelf)
        {   var bookInfos = new List<GoogleBookInfo>();
            using BooksService booksService = new BooksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "My Google Bookshelf App"
            });
            var booksRequest = booksService.Mylibrary.Bookshelves.Volumes.List(targetShelf.Id.Value.ToString());
            Volumes books = await booksRequest.ExecuteAsync().ConfigureAwait(false);

            if (books.Items != null && books.Items.Count > 0)
            {
                Console.WriteLine($"Books in '{targetShelf.Title}':");
                foreach (Google.Apis.Books.v1.Data.Volume book in books.Items)
                {/*      Id = id; Title = title; Authors = authors;
                    PublishedDate = publishedDate; Description = des; PageCount = count; Thumbnail = link;*/
                    string id = book.Id;
                    string title = book.VolumeInfo.Title;

                    string[] authors; 
                    var x = book.VolumeInfo.Authors;
                    if (x != null) { if (x.Count > 0) { authors = x.ToArray(); } else authors = ["N/A"]; }
                    else authors = ["N/A"];

                    string publishedDate = book.VolumeInfo.PublishedDate;
                    if (publishedDate == null) publishedDate = "N/A";

                    string des = book.VolumeInfo.Description;
                    if (des == null) des = "N/A";

                    int count = book.VolumeInfo.PageCount.HasValue ? book.VolumeInfo.PageCount.Value : 0;
                    var links = book.VolumeInfo.ImageLinks;
                    string thumbnail;
                    if (links != null) thumbnail = links.Thumbnail;
                    else thumbnail = "N/A";
                    bookInfos.Add(new GoogleBookInfo(id, title, authors, publishedDate, des, count, thumbnail));
                }
            }
            else
            {
               // Console.WriteLine($"No books found in bookshelf '{targetShelf.Title}'.");
            }
            return bookInfos;
        }

        public static async Task<List<BookUserControl>> GetBookUserControls(UserCredential credential, Bookshelf shelf
            , BrowseBookshelfForm form)
        {
            List<GoogleBookInfo> bookInfos =await GetMyGoogleBooks(credential,shelf).ConfigureAwait(false);
            int totalItems = bookInfos.Count;
            int done = 0;
            List<BookUserControl> bookUCs = new();
            foreach (var bookInfo in bookInfos)
            {
                Image im;
                if (!bookInfo.Thumbnail.Equals("N/A"))
                    im = await DownloadPosterImage(bookInfo.Thumbnail).ConfigureAwait(false); //
                else im = null;
                bookUCs.Add(new BookUserControl(shelf,bookInfo, im));
                done++;
                form.UpdateSearchProgress(done * 100 / totalItems);
            }


            return bookUCs;
        }

        private static string GetWifiIPv4Address()
        {
            // Get all network interfaces
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            // Find the wireless network interface
            var wirelessInterface = networkInterfaces
                .FirstOrDefault(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                && ni.OperationalStatus == OperationalStatus.Up);
            if (wirelessInterface != null)
            {
                // Get the IP properties of the interface
                var ipProperties = wirelessInterface.GetIPProperties();
                // Get the IPv4 address of the interface
                var ipv4Address = ipProperties.UnicastAddresses
                    .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)?.Address;
                if (ipv4Address != null) return ipv4Address.ToString();
                else return "127.0.0.1";
            }
            else return "127.0.0.1";
        }
        public static bool IsConnectedToTheInternet()
        {
            return (!GetWifiIPv4Address().Equals("127.0.0.1"));
        }
    }
}
