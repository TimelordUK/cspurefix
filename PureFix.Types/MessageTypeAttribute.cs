using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Holds information on the FIX message
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessageTypeAttribute : FixAttribute
    {
        public MessageTypeAttribute(string type, FixVersion version)
        {
            this.Type = type;
        }

        /// <summary>
        /// The FIX type string for the message
        /// </summary>
        public string Type{get;}

        /// <summary>
        /// The FIX version the message adheres to
        /// </summary>
        public FixVersion Version{get;}
    }
}
