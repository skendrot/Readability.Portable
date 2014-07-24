using Newtonsoft.Json;
using System.Collections.Generic;
using PropertyChanged;

namespace Readability.Models
{
    [ImplementPropertyChanged]
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
