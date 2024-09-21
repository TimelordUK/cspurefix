using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public interface IFixMsgResender
    {
        public Task<IReadOnlyList<IFixMsgStoreRecord>> GetResendRequest(int startReq, int endSeq);
    }
}
