using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessageTypeAttribute : Attribute
    {
        public MessageTypeAttribute(string type, FixVersion version)
        {
            this.Type = type;
        }

        public string Type{get;}

        public FixVersion Version{get;}
    }
}
