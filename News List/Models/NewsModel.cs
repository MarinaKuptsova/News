using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News_List.Models
{
    public class NewsModel: NewsItem
    {
        public Dictionary<long, string> Source { get; set; }
    }
}