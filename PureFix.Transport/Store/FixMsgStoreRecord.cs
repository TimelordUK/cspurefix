using PureFix.Buffer.Ascii;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public class FixMsgStoreRecord : IFixMsgStoreRecord
    {
        public FixMsgStoreRecord(string msgType, DateTime timestamp, int seqNum, string? encoded = null)
        {
            MsgType = msgType;
            Timestamp = timestamp;
            SeqNum = seqNum;
            Encoded = encoded;
        }

        public string MsgType { get; }
        public DateTime Timestamp { get; }
        public int SeqNum { get; }
        public IFixMessage? InflatedMessage { get; set; }
        public string? Encoded { get; }

        public static IFixMsgStoreRecord ToMsgStoreRecord(IMessageView v)
        {
            return new FixMsgStoreRecord(v.MsgType() ?? "", v.SendingTime() ?? DateTime.MinValue, v.MsgSeqNum() ?? -1);
        }

        public IFixMsgStoreRecord Clone()
        {
            return new FixMsgStoreRecord(MsgType, Timestamp, SeqNum, Encoded);
        }
    }
}
