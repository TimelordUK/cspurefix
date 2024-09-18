using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public class FixSessionStateArgs
    {
        public int HeartBeat { get; set; }
        public SessionState? State { get; set; }
        public int? WaitLogoutConfirmSeconds { get; set; }
        public int? StopSeconds { get; set; }
        public int? LastPeerMsgSeqNum { get; set; }
    }
}
