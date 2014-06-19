using System;

using Newtonsoft.Json;
using Readability.JsonConverters;

namespace Readability.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Conditions
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("opened_since")]
        public DateTime? OpenedSince { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("added_until")]
        public DateTime? AddedUntil { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("opened_until")]
        public DateTime? OpenedUntil { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("archived_until")]
        public DateTime? ArchivedUntil { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("favorite")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool? Favorite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("archived_since")]
        public DateTime? ArchivedSince { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("favorited_since")]
        public DateTime? FavoritedSince { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("favorited_until")]
        public DateTime? FavoritedUntil { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("archive")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool? Archive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("added_since")]
        public DateTime? AddedSince { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("order")]
        public string Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("page")]
        public int? Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("updated_since")]
        public DateTime? UpdatedSince { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("updated_until")]
        public DateTime? UpdatedUntil { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("only_deleted")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool? OnlyDeleted { get; set; }
    }
}
