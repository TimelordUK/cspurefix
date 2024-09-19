using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.Config;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types
{
    public class Fix44SessionMessageFactory : ISessionMessageFactory
    {
        private readonly SessionDescription m_SessionDescription;

        public Fix44SessionMessageFactory(SessionDescription sessionDescription)
        {
            m_SessionDescription = sessionDescription;
        }

        public virtual IFixMessage? TestRequest(string testReqId)
        {
            return new TestRequest { TestReqID = testReqId };
        }

        public virtual IFixMessage? Heartbeat(string testReqId)
        {
            return new Heartbeat { TestReqID = testReqId };
        }

        public virtual IFixMessage? ResendRequest(int beginSeqNo, int endSeqNo)
        {
            return new ResendRequest { BeginSeqNo = beginSeqNo, EndSeqNo = endSeqNo };
        }
        public virtual IFixMessage? SequenceReset(int newSeqNo, bool? gapFill = null)
        {
            return new SequenceReset { GapFillFlag = gapFill, NewSeqNo = newSeqNo };
        }

        public virtual IStandardTrailer? Trailer(int checksum)
        {
            return new StandardTrailerComponent() { CheckSum = checksum.ToString("D3") };
        }

        public virtual IFixMessage? Logon(string? userRequestId, bool? isResponse)
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

        public virtual IFixMessage? Reject(string msgType, int seqNo, string msg, int reason)
        {
            return new Reject
            {
                RefMsgType = msgType,
                SessionRejectReason = reason,
                RefSeqNum = seqNo,
                Text = msg
            };
        }

        public virtual IFixMessage? Logout(string text)
        {
            return new Logout
            {
                Text = text
            };
        }

        public IStandardHeader? Header(string msgType, int seqNum, DateTime time, IStandardHeader? overrides = null)
        {
            var bodyLength = Math.Max(4, m_SessionDescription.BodyLengthChars ?? 7);
            var placeholder = (int)Math.Pow(10, bodyLength - 1) + 1;
            return new StandardHeaderComponent
            {
                BeginString = m_SessionDescription.BeginString,
                BodyLength = placeholder,
                MsgType = msgType,
                SenderCompID = m_SessionDescription.SenderCompID,
                MsgSeqNum = seqNum,
                SendingTime = time,
                TargetCompID = m_SessionDescription.TargetCompID,
                TargetSubID = m_SessionDescription.TargetSubID,
                SenderSubID = m_SessionDescription.SenderSubID
            };
        }
    }
}
