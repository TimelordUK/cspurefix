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
        void ParseFrom(ReadOnlySpan<byte> readFrom, int len, Action<int, MsgView>? onView, Action<StoragePool.Storage>? onDecode = null);
        void Return(StoragePool.Storage sto);
    }
}
