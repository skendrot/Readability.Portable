using System;
using Newtonsoft.Json;
using PropertyChanged;

namespace Readability.Models
{
    [ImplementPropertyChanged]
    public class Article
    {
        public string Domain { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        [JsonProperty("lead_image_url")]
        public string LeadImageUrl { get; set; }
        public string Author { get; set; }
        public string Excerpt { get; set; }
        public string Direction { get; set; }

        [JsonProperty("word_count")]
        public int WordCount { get; set; }
        public string Content { get; set; }

        [JsonProperty("date_published")]
        public DateTime? DatePublished { get; set; }
        public object Dek { get; set; }
        public bool Processed { get; set; }
        public string Id { get; set; }
    }
}