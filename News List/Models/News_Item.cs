using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News_List.Models
{
    public class NewsItem
    {
        public long Id { get; set; }

        public long SourceId { get; set; }

        public string SourceName { get; set; }

        public DateTime CreateTime { get; set; }
        
        public string Contents { get; set; }

        public long ThemeId { get; set; }

        public string ThemeName { get; set; }
                
        public string Name { get; set; }

        public string Search { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}