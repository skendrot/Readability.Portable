using AsyncOAuth;
using Newtonsoft.Json;
using Readability.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Readability
{
    public partial class ReadabilityClient
    {
        const string ArticleUrl = BaseUrl + "articles";

        public async Task<Article> GetArticleAsync(string articleId)
        {
            ValidateAccessToken();
            
            string url = string.Format("{0}/{1}", ArticleUrl, articleId);
            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            var json = await client.GetStringAsync(url).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Article>(json);
        }
    }
}
