using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Transport;
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

        IFixMessage? ISessionMessageFactory.TestRequest(string testReqId)
        {
            return new TestRequest { StandardHeader = new StandardHeaderComponent(), TestReqID = testReqId };
        }

        IFixMessage? ISessionMessageFactory.Heartbeat(string testReqId)
        {
            return new Heartbeat { StandardHeader = new StandardHeaderComponent(), TestReqID = testReqId };
        }

        IFixMessage? ISessionMessageFactory.ResendRequest(int beginSeqNo, int endSeqNo)
        {
            return new ResendRequest { StandardHeader = new StandardHeaderComponent(), BeginSeqNo = beginSeqNo, EndSeqNo = endSeqNo };
        }
        IFixMessage? ISessionMessageFactory.SequenceReset(int newSeqNo, bool? gapFill)
        {
            return new SequenceReset { StandardHeader = new StandardHeaderComponent(), GapFillFlag = gapFill, NewSeqNo = newSeqNo };
        }

        IStandardTrailer? ISessionMessageFactory.Trailer(int checksum)
        {
            return new StandardTrailerComponent() { CheckSum = checksum.ToString("D3") };
        }

        IFixMessage? ISessionMessageFactory.Logon(string? userRequestId, bool? isResponse)
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

        IFixMessage? ISessionMessageFactory.Reject(string msgType, int seqNo, string msg, int reason)
        {
            return new Reject
            {
                RefMsgType = msgType,
                SessionRejectReason = reason,
                RefSeqNum = seqNo,
                Text = msg
            };
        }

        IFixMessage? ISessionMessageFactory.Logout(string text)
        {
            return new Logout
            {
                Text = text
            };
        }

        IStandardHeader? ISessionMessageFactory.Header(string msgType, int seqNum, DateTime time, IStandardHeader? overrides)
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
