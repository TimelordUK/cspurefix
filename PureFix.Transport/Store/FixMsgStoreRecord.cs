using PureFix.Buffer.Ascii;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PureFix.Transport.Store
{
    internal class FixMsgStoreRecord : IFixMsgStoreRecord
    {
        public FixMsgStoreRecord(string msgType, DateTime timestamp, int seqNum, IFixMessage? obj, string? encoded)
        {
            MsgType = msgType;
            Timestamp = timestamp;
            SeqNum = seqNum;
            Obj = obj;
            Encoded = encoded;
        }

        public string MsgType { get; }

        public DateTime Timestamp { get; }

        public int SeqNum { get; }

        public IFixMessage? Obj { get; set; }

        public string? Encoded { get; }

       // public static IFixMsgStoreRecord ToMsgStoreRecord(MsgView v) {
       //     return new FixMsgStoreRecord(v.MsgType() ?? "", v.SendingTime(), v.MsgSeqNum, v.toObject() as ILooseObject);
       // }
    }
}
