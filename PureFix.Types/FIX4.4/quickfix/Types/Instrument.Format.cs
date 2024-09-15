using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX44.QuickFix.Types
{
    public partial class Instrument
    {
        void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
        {
            if (Symbol != null)
            {
                var at = storage.Pos;
                storage.WriteWholeNumber(55);
                storage.WriteChar((byte)'=');
                storage.WriteString(Symbol);
                storage.WriteChar(delimiter);
                tags.Store(at, storage.Pos - at, 55);
            }
        }
    }
}
