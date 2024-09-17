using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IStandardHeader : IFixParser, IFixEncoder
    {
        public string? BeginString { get; }
        public int? BodyLength { get; }
        public string? MsgType{get;}
        public string? SenderCompID { get; }
        public int? MsgSeqNum { get; }
        public DateTime? SendingTime { get; }
        public string? TargetCompID { get; }
        public string? TargetSubID { get; }
        public string? SenderSubID { get; }
        public DateTime? OrigSendingTime { get; }
    }
}
