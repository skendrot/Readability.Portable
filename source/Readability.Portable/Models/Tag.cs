using System.Collections.Generic;
using Newtonsoft.Json;

namespace Readability.Models
{
    public class Tag
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("applied_count")]
        public int AppliedCount { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("bookmark_ids")]
        public List<int> BookmarkIDs { get; set; }
    }
}