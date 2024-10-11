using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PureFix.Buffer
{
    public interface IMessageEncoder
    {
        StoragePool.Storage? Encode(string msgType, IFixMessage message);
        void Return(StoragePool.Storage storage);
        int MsgSeqNum { get; set; }
    }
}
