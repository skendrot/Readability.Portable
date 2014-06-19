using AsyncOAuth;
using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Readability
{
    public class ReadabilityClient
    {
        private const string BaseUrl = "https://www.readability.com/api/rest/v1/";
        private const string AuthUrl = BaseUrl + "oauth";
        private const string BookmarkUrl = BaseUrl + "bookmarks";
        private const string ProfileUrl = BaseUrl + "users/_current";

        private readonly string _consumerKey;
        private readonly string _consumerSecret;

        private ReadabilityClient()
        {
            OAuthUtility.ComputeHash = (key, buffer) =>
            {
                var crypt = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
                var keyBuffer = WinRTCrypto.CryptographicBuffer.CreateFromByteArray(key);
                var cryptKey = crypt.CreateKey(keyBuffer);

                var dataBuffer = WinRTCrypto.CryptographicBuffer.CreateFromByteArray(buffer);
                var signBuffer = WinRTCrypto.CryptographicEngine.Sign(cryptKey, dataBuffer);

                byte[] value;
                WinRTCrypto.CryptographicBuffer.CopyToByteArray(signBuffer, out value);
                return value;
            };
        }

        public ReadabilityClient(string consumerKey, string consumerSecret, string oauthToken = null, string oauthSecret = null) : this()
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;

            if ((string.IsNullOrEmpty(oauthToken) == false) && (string.IsNullOrEmpty(oauthSecret) == false))
            {
                AccessToken = new AccessToken(oauthToken, oauthSecret);
            }
        }

        public AccessToken AccessToken { get; set; }

        public async Task<string> GetRequestTokenAsync(string callbackUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            //
            // Acquiring a request token
            //

            string authUrl = AuthUrl + "/request_token/";

            var parameters = new Dictionary<string, string> { { "oauth_callback", callbackUri } };

            var authorizer = new OAuthAuthorizer(_consumerKey, _consumerSecret);
            TokenResponse<RequestToken> tokenResponse = await authorizer.GetRequestToken(authUrl, parameters).ConfigureAwait(false);
            AccessToken = new AccessToken(tokenResponse.Token.Key, tokenResponse.Token.Secret);
            return tokenResponse.Token.Key;
        }

        public Uri GenerateAuthenticationUri()
        {
            if (AccessToken == null) throw new Exception();
            if (string.IsNullOrEmpty(AccessToken.Key)) throw new Exception();

            string readerUrl = AuthUrl + "/authorize?oauth_token=" + AccessToken.Key;
            var authUri = new Uri(readerUrl);
            return authUri;
        }

        public async Task<AccessToken> VerifyUserAsync(string verifier)
        {
            if (AccessToken == null) throw new Exception();
            if (string.IsNullOrEmpty(AccessToken.Key)) throw new Exception();
            if (string.IsNullOrEmpty(AccessToken.Secret)) throw new Exception();

            const string accessUrl = AuthUrl + "/access_token/";

            OAuthAuthorizer authorizer = new OAuthAuthorizer(_consumerKey, _consumerSecret);
            TokenResponse<AccessToken> response = await authorizer.GetAccessToken(accessUrl, new RequestToken(AccessToken.Key, AccessToken.Secret), verifier).ConfigureAwait(false);
            AccessToken = response.Token;
            return AccessToken;
        }

        public async Task<UserProfile> GetProfileAsync()
        {
            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            HttpResponseMessage response = await client.GetAsync(ProfileUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(json);
            }
            return null;
        }

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
    }
}
