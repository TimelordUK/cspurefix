using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class GroupAttribute : Attribute
    {
        public GroupAttribute(int noOfTag)
        {
            this.NoOfTag = noOfTag;
        }

        public int NoOfTag{get;}
    }
}
