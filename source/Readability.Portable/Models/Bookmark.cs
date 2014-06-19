using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Readability.Models
{
    public class Bookmark
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("read_percent")]
        public string ReadPercent { get; set; }

        [JsonProperty("date_updated")]
        public DateTime DateUpdated { get; set; }

        public bool Favorite { get; set; }
        
        public Article Article { get; set; }
        
        public int Id { get; set; }

        [JsonProperty("date_archived")]
        public DateTime? DateArchived { get; set; }

        // Deprecated
        //[JsonProperty("date_opened")]
        //public DateTime DateOpened { get; set; }

        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("article_href")]
        public string ArticleUrl { get; set; }

        [JsonProperty("date_favorited")]
        public DateTime? DateFavorited { get; set; }
        
        public bool Archive { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
