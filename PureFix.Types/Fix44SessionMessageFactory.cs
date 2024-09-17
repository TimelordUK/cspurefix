using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types
{
    public class Fix44SessionMessageFactory : ISessionMessageFactory
    {
        private SessionDescription m_SessionDescription;

        public Fix44SessionMessageFactory(SessionDescription sessionDescription)
        {
            m_SessionDescription = sessionDescription;
        }

        public IFixMessage? TestRequest(string testReqId)
        {
            return new TestRequest { TestReqID = testReqId };
        }

        public IFixMessage? Heartbeat(string testReqId)
        {
            return new Heartbeat { TestReqID = testReqId };
        }

        public IFixMessage? ResendRequest(int beginSeqNo, int endSeqNo)
        {
            return new ResendRequest { BeginSeqNo = beginSeqNo, EndSeqNo = endSeqNo };
        }
        public IFixMessage? SequenceReset(int newSeqNo, bool? gapFill = null)
        {
            return new SequenceReset { GapFillFlag = gapFill, NewSeqNo = newSeqNo };
        }

        public IStandardTrailer? Trailer(int checksum)
        {
            return new StandardTrailerComponent() { CheckSum = checksum.ToString("D3") };
        }

        public IFixMessage? Logon(string userRequestId, bool isResponse)
        {
            return new Logon
            {
                Username = m_SessionDescription.Username,
                Password = m_SessionDescription.Password, 
                HeartBtInt = m_SessionDescription.HeartBtInt,
                ResetSeqNumFlag = m_SessionDescription.ResetSeqNumFlag,
                EncryptMethod = EncryptMethodValues.None
            };
        }

        public IFixMessage? Reject(string msgType, int seqNo, string msg, int reason)
        {
            return new Reject
            {
                RefMsgType = msgType,
                SessionRejectReason = reason,
                RefSeqNum = seqNo,
                Text = msg
            };
        }

        public IFixMessage? Logout(string text)
        {
            return new Logout
            {
                Text = text
            };
        }

        public IStandardHeader? Header(string msgType, int seqNum, DateTime time, IStandardHeader? overrides = null)
        {
            return null;
        }
    }
}
