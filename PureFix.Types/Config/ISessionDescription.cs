using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.Config
{
    public interface ISessionDescription
    {
        public MsgApplication? Application { get; }
        public string? Name { get; }
        public string? SenderCompID { get; }
        public string? TargetCompID { get; }
        public bool? ResetSeqNumFlag { get; }
        public string? SenderSubID { get; }
        public string? TargetSubID { get; }
        public string? BeginString { get; }
        public string? Username { get; }
        public string? Password { get; }
        public int? LastSentSeqNum { get; }
        public int? LastReceivedSeqNum { get; }
        public int? BodyLengthChars { get; }
        public int? HeartBtInt { get; }
    }
}
