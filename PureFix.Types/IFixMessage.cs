using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Defines a generic fix message which is common across all versions
    /// </summary>
    public interface IFixMessage : IFixEncoder, IFixValidator, IFixParser, IFixLookup
    {
        /// <summary>
        /// Returns the generic standard header
        /// </summary>
        public IStandardHeader? StandardHeader{get;}

        /// <summary>
        /// Returns the standard generic trailer
        /// </summary>
        public IStandardTrailer? StandardTrailer{get;}

        /// <summary>
        /// Returns the message type
        /// </summary>
        public string? MsgType
        {
            get{return StandardHeader?.MsgType;}
        }
    }
}
