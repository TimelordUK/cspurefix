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
    public sealed class GroupAttribute : FixAttribute, ITagOffset
    {
        public GroupAttribute()
        {
        }

        /// <summary>
        /// The field which holds the count for the group
        /// </summary>
        public required int NoOfTag{get; init;}

        /// <summary>
        /// The zero-based position of the field within the message, component or group
        /// </summary>
        public required int Offset{get; init;}
    }
}
