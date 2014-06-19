using System.Collections.Generic;

namespace Readability.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BookmarksResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Bookmark> Bookmarks { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Meta Meta { get; set; }
    }
}
