using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide
{
    public class GoogleBookInfo
    {
        public string Id {  get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Thumbnail {  get; set; }

        public GoogleBookInfo() { }
        public GoogleBookInfo(string id, string title, string[] authors
            , string publishedDate, string des, int count, string link) 
        {
        Id = id; Title = title; Authors = authors; 
            PublishedDate = publishedDate; Description = des; PageCount = count; Thumbnail = link;       
        }

    }
}
