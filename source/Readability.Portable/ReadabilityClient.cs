using AsyncOAuth;
using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Readability.Models;


namespace Readability
{
    /// <summary>
    /// Represents a client for the Readability API.
    /// </summary>
    public partial class ReadabilityClient : IReadabilityClient
    {
        private const string BaseUrl = "https://www.readability.com/api/rest/v1/";
        private const string AuthUrl = BaseUrl + "oauth";
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

        /// <summary>
        /// Creates an instance of the ReadabilityClient.
        /// </summary>
        /// <param name="consumerKey">The Readability consumer key.</param>
        /// <param name="consumerSecret">The Readability consumer secret.</param>
        /// <param name="oauthToken">An oauth token from a previous authentication.</param>
        /// <param name="oauthSecret">An oauth seret from a previous authentication.</param>
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
    }
}
