using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncOAuth;
using Readability.Models;

namespace Readability
{
    public interface IReadabilityClient
    {
        AccessToken AccessToken { get; set; }
        Task<string> GetRequestTokenAsync(string callbackUri, CancellationToken cancellationToken = default(CancellationToken));
        Uri GenerateAuthenticationUri();
        Task<AccessToken> VerifyUserAsync(string verifier);
        Task<UserProfile> GetProfileAsync();
        Task<Article> GetArticleAsync(int articleId);
        Task<bool> AddBookmarkAsync(string url, bool favorite = false, bool archive = false);
        Task<bool> DeleteBookmarkAsync(int bookmarkId);
        Task<BookmarksResponse> GetBookmarksAsync(Conditions conditions);
        Task<Bookmark> GetBookmarkAsync(int bookmarkId);
    }
}