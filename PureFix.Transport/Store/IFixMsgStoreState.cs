using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public readonly record struct FixMsgStoreState(int Length, int FirstSeq, int LastSeq, int ID);
}
