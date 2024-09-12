using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TagDetailsAttribute : Attribute
    {
        public TagDetailsAttribute(int tag, TagType type)
        {
            this.Tag = tag;
            this.Type = type;
        }

        public int Tag{get;}

        public TagType Type{get;}
    }
}
