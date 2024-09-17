using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Looks up tag data
    /// </summary>
    public interface IFixLookup
    {
        /// <summary>
        /// Attempts to get the data for a tag
        /// </summary>
        /// <param name="tag">The tag to lookup</param>
        /// <param name="value">If the tag exists then the value held by it, otherwise null</param>
        /// <returns>true if the tag exists, false otherwise</returns>
        public bool TryGetByTag(string name, out object? value);
    }
}
