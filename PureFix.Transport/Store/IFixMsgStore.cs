using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public interface IFixMsgStore
    {
        public Task<IFixMsgStoreState> Clear();
        public Task<IFixMsgStoreState> GetState();
        public Task<IFixMsgStoreState> Put(IFixMsgStoreRecord record);
        public Task<IFixMsgStoreRecord> Get(int seq);
        public Task<bool> Exists(int seq);
        public Task<FixMsgStoreRecord[]> GetSeqNumRange(int from, int?to = null);
    }
}
