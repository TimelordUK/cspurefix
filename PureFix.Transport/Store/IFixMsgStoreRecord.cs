using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public interface IFixMsgStoreRecord
    {
        string MsgType { get; }
        DateTime Timestamp { get; }
        int SeqNum { get; }
        string? Encoded { get; }
        IFixMessage? InflatedMessage { get; set; }
        IFixMsgStoreRecord Clone();
    }
}
