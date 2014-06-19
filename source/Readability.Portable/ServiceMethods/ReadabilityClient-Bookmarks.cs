using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AsyncOAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Readability.Models;

namespace Readability
{
    public partial class ReadabilityClient
    {
        private const string BookmarkUrl = BaseUrl + "bookmarks";

        public async Task<bool> AddBookmarkAsync(string url, bool favorite = false, bool archive = false)
        {
            var contentParams = new Dictionary<string, string>();
            contentParams.Add("url", url);
            if (favorite)
            {
                contentParams.Add("favorite", "1");
            }
            if (archive)
            {
                contentParams.Add("archive", "1");
            }

            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            var content = new FormUrlEncodedContent(contentParams);
            var httpResponseMessage = await client.PostAsync(BookmarkUrl, content).ConfigureAwait(false);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookmark(int bookmarkId)
        {
            string url = string.Format("{0}/{1}", BookmarkUrl, bookmarkId);
            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            var message = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = await client.SendAsync(message).ConfigureAwait(false);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<BookmarksResponse> GetBookmarksAsync(Conditions conditions)
        {
            var jsonSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            JObject jObject = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(conditions, jsonSettings), new JsonSerializerSettings { DateParseHandling = DateParseHandling.None});

            string url = BookmarkUrl;
            string query = string.Join("&", jObject.Select<KeyValuePair<string, JToken>, string>(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                url = string.Format("{0}?{1}", url, query);
            }

            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            var json = await client.GetStringAsync(url).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<BookmarksResponse>(json);
            return null;
        }

        public async Task<Bookmark> GetBookmarkAsync(int bookmarkId)
        {
            string url = string.Format("{0}/{1}", BookmarkUrl, bookmarkId);
            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            var json = await client.GetStringAsync(url).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Bookmark>(json);
        }
    }
}
