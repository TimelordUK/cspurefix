using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Flags a property as being a FIX field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class GroupAttribute : FixAttribute
    {
        public GroupAttribute(int noOfTag)
        {
            this.NoOfTag = noOfTag;
        }

        /// <summary>
        /// The field which holds the count for the group
        /// </summary>
        public int NoOfTag{get;}
    }
}
