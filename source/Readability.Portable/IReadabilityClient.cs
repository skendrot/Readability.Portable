using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncOAuth;
using Readability.Models;

namespace Readability
{
    /// <summary>
    /// Represents a client for the Readability API.
    /// </summary>
    public interface IReadabilityClient
    {
        /// <summary>
        /// Gets or sets the <see cref="AccessToken"/>
        /// </summary>
        AccessToken AccessToken { get; set; }
        
        /// <summary>
        /// Get the request token from Readability.
        /// </summary>
        /// <param name="callbackUri">The Uri to send the request back to.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A Task with the response that will complete when the request completes.</returns>
        Task<string> GetRequestTokenAsync(string callbackUri, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Get the uri to authenticate with.
        /// </summary>
        /// <returns></returns>
        Uri GenerateAuthenticationUri();
        
        /// <summary>
        /// Verify the response from the authenication.
        /// </summary>
        /// <param name="verifier"></param>
        /// <returns>A Task with the access that will complete when the request completes.</returns>
        Task<AccessToken> VerifyUserAsync(string verifier);

        /// <summary>
        /// Get the user profile.
        /// </summary>
        /// <returns>A Task with the UserProfile.</returns>
        /// <remarks>
        /// The AccessToken must the set.
        /// </remarks>
        Task<UserProfile> GetProfileAsync();

        /// <summary>
        /// Get an article with the given articleId.
        /// </summary>
        /// <param name="articleId">The id of the article.</param>
        /// <returns>A Task with the Article.</returns>
        Task<Article> GetArticleAsync(string articleId);

        /// <summary>
        /// Add a Bookmark for the authenticated user.
        /// </summary>
        /// <param name="url">The url the bokmark will represent.</param>
        /// <param name="favorite">An optional value indicating if the bookmark should be a favorite.</param>
        /// <param name="archive">An optional value indicating if the bookmark should be archived.</param>
        /// <returns></returns>
        /// <remarks>
        /// The AccessToken must be set.
        /// </remarks>
        Task<bool> AddBookmarkAsync(string url, bool favorite = false, bool archive = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookmarkId"></param>
        /// <returns></returns>
        /// <remarks>
        /// The AccessToken must be set.
        /// </remarks>
        Task<bool> DeleteBookmarkAsync(int bookmarkId);

        /// <summary>
        /// Get the bookmarks for the authenticated user.
        /// </summary>
        /// <param name="conditions">The conditions in which to get bookmarks.</param>
        /// <returns>A Task with the users Bookmarks.</returns>
        /// <remarks>
        /// The AccessToken must be set.
        /// </remarks>
        Task<BookmarksResponse> GetBookmarksAsync(Conditions conditions);
        
        /// <summary>
        /// Gets the bookmark with the given bookmarkId.
        /// </summary>
        /// <param name="bookmarkId">The id of the bookmark.</param>
        /// <returns>A Task with the Bookmark</returns>
        /// <remarks>
        /// The AccessToken must be set.
        /// </remarks>
        Task<Bookmark> GetBookmarkAsync(int bookmarkId);
    }
}