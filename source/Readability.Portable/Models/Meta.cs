
using Newtonsoft.Json;

namespace Readability.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("num_pages")]
        public int NumPages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("item_count_total")]
        public int ItemCountTotal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("item_count")]
        public int ItemCount { get; set; }
    }
}
