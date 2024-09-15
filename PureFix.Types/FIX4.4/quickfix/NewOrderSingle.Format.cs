using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX44.QuickFix
{
    public sealed partial class NewOrderSingle
    {
        void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
        {
            if (ClOrdID != null)
            {
                var at = storage.Pos;
                storage.WriteWholeNumber(11);
                storage.WriteChar((byte)'=');
                storage.WriteString(ClOrdID);
                storage.WriteChar(delimiter);
                tags.Store(at, storage.Pos - at, 11);
            }

            if (Side != null)
            {
                var at = storage.Pos;
                storage.WriteWholeNumber(54);
                storage.WriteChar((byte)'=');
                storage.WriteString(Side);
                storage.WriteChar(delimiter);
                tags.Store(at, storage.Pos - at, 54);
            }

            ((IFixEncoder)OrderQtyData!)?.Encode(storage, tags, delimiter);

            if (Instrument != null)
            {
                ((IFixEncoder)Instrument!)?.Encode(storage, tags, delimiter);
            }

            if (OrdType != null)
            {
                var at = storage.Pos;
                storage.WriteWholeNumber(38);
                storage.WriteChar((byte)'=');
                storage.WriteString(OrdType);
                storage.WriteChar(delimiter);
                tags.Store(at, storage.Pos - at, 38);
            }

            if (Price != null)
            {
                var at = storage.Pos;
                storage.WriteWholeNumber(44);
                storage.WriteChar((byte)'=');
                storage.WriteNumber(Price.Value);
                storage.WriteChar(delimiter);
                tags.Store(at, storage.Pos - at, 44);
            }

            if (TimeInForce != null)
            {
                var at = storage.Pos;
                storage.WriteWholeNumber(59);
                storage.WriteChar((byte)'=');
                storage.WriteString(TimeInForce);
                storage.WriteChar(delimiter);
                tags.Store(at, storage.Pos - at, 59);
            }

            if (Account != null)
            {
                var at = storage.Pos;
                storage.WriteWholeNumber(1);
                storage.WriteChar((byte)'=');
                storage.WriteString(Account);
                storage.WriteChar(delimiter);
                tags.Store(at, storage.Pos - at, 1);
            }
        }
    }
}
