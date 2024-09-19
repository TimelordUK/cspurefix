using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;


namespace PureFix.Buffer
{
    public interface IMessageParser
    {
        void ParseFrom(ReadOnlySpan<byte> readFrom, Action<int, MsgView>? onView, Action<ElasticBuffer>? onDecode = null);
    }
}
