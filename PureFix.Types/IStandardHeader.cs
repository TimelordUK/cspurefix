using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IStandardHeader : IFixParser, IFixEncoder, IFixReset
    {
        public string? BeginString { get; set; }
        public int? BodyLength { get; set; }
        public string? MsgType{ get; set; }
        public string? SenderCompID { get; set; }
        public int? MsgSeqNum { get; set; }
        public DateTime? SendingTime { get; set; }
        public string? TargetCompID { get; set; }
        public string? TargetSubID { get; set; }
        public string? SenderSubID { get; set; }
        public DateTime? OrigSendingTime { get; set; }
        public bool? PossDupFlag { get; set; }
    }
}
