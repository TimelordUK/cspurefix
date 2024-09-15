using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IFixEncoder
    {
        public void Encode(ElasticBuffer storage, Tags tags, byte delimiter) { }
    }

    /// <summary>
    /// Defines a generic fix message which is common across all versions
    /// </summary>
    public interface IFixMessage : IFixEncoder
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
