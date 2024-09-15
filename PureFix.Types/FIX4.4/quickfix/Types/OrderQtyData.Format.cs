using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX44.QuickFix.Types
{
    public sealed partial class OrderQtyData
    {
        void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
        {
        }
    }
}
