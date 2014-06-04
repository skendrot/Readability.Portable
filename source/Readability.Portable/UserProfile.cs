using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Readability
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

    public class UserProfile
    {
        [JsonProperty("username")]
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("date_joined")]
        public string DateJoined { get; set; }

        [JsonProperty("has_active_subscription")]
        public bool HasActiveSubscription { get; set; }

        [JsonProperty("reading_limit")]
        public int ReadingLimit { get; set; }

        [JsonProperty("email_into_address")]
        public string EmailIntoAddress { get; set; }

        [JsonProperty("kindle_email_address")]
        public string KindleEmailAddress { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
    }
}
