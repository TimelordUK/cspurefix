using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Holds information on an individual tag
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TagDetailsAttribute : FixAttribute, ITagOffset
    {
        public TagDetailsAttribute()
        {
        }

        /// <summary>
        /// The FIX tag number
        /// </summary>
        public required int Tag{get; init;}

        /// <summary>
        /// The FIX type of the field
        /// </summary>
        public required TagType Type{get;  init;}

        /// <summary>
        /// The zero-based position of the field within the message, component or group
        /// </summary>
        public required int Offset{get; init;}

        public required bool Required{get; init;}

        /// <summary>
        /// The FIX tag of a field that this message is linked to in some way, or -1 if there is no such field.
        /// This is typically used to associate raw data fields with their length field
        /// </summary>
        public int LinksToTag{get; init;} = -1;
    }
}
