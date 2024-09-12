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
    public sealed class TagDetailsAttribute : FixAttribute
    {
        public TagDetailsAttribute(int tag, TagType type)
        {
            this.Tag = tag;
            this.Type = type;
        }

        /// <summary>
        /// The FIX tag number
        /// </summary>
        public int Tag{get;}

        /// <summary>
        /// The FIX type of the field
        /// </summary>
        public TagType Type{get;}
    }
}
