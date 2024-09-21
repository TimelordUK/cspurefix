using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public interface IFixMsgStoreState
    {
        int Length { get; }
        int FirstSeq { get; }
        int LastSeq { get; }
        int ID { get; }
    }
}
