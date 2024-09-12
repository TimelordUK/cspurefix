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
        public TagDetailsAttribute(int tag)
        {
            this.Tag = tag;
        }

        public int Tag{get;}
    }
}
