using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface ISessionMessageFactory
    {
        public IFixMessage? TestRequest(string testReqId)
        {
            return null;
        }

        public IFixMessage? Heartbeat(string testReqId)
        {
            return null;
        }

        public IFixMessage? ResendRequest(int from, int to)
        {
            return null;
        }
        public IFixMessage? SequenceReset(int newSeqNo, bool? gapFill = null)
        {
            return null;
        }

        public IStandardTrailer? Trailer(int checksum)
        {
            return null;
        }

        public IFixMessage? Logon(string? userRequestId = null, bool isResponse = false)
        {
            return null;
        }

        public IFixMessage? Reject(string msgType, int seqNo, string msg, int reason)
        {
            return null;
        }


        public IFixMessage? Logout(string msgType, string text)
        {
            return null;
        }

        public IStandardHeader? Header(string msgType, int seqNum, DateTime time, IStandardHeader? overrides = null)
        {
            return null;
        }
    }
}
