using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readability.Models
{
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
