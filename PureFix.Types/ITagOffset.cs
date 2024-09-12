using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface ITagOffset
    {
        /// <summary>
        /// The zero-based position of the field within the message, component or group
        /// </summary>
        public int Offset{get;}
    }
}
