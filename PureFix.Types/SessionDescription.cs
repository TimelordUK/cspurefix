using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public class SessionDescription
    {
        public MsgApplication? Application { get; set; }
        public string? Name { get; set; }
        public string? SenderCompID { get; set; }
        public string? TargetCompID { get; set; }
        public bool? ResetSeqNumFlag { get; set; }
        public string? SenderSubID { get; set; }
        public string? TargetSubID { get; set; }
        public string? BeginString { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? LastSentSeqNum { get; set; }
        public int? LastReceivedSeqNum { get; set; }
        public int? BodyLengthChars { get; set; }
    }
}
