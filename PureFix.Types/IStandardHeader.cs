using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IStandardHeader : IFixParser, IFixEncoder, IFixReset
    {
        public string? BeginString { get; }
        public int? BodyLength { get; }
        public string? MsgType{get;}
        public string? SenderCompID { get; }
        public int? MsgSeqNum { get; set; }
        public DateTime? SendingTime { get; set; }
        public string? TargetCompID { get; }
        public string? TargetSubID { get; }
        public string? SenderSubID { get; }
        public DateTime? OrigSendingTime { get; set; }
        public bool? PossDupFlag { get; set; }
    }
}
