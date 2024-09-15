using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PreAllocGrpNoAllocs
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (AllocAccount != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(79);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)AllocAccount);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 79);
			}
			if (AllocAcctIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(661);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)AllocAcctIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 661);
			}
			if (AllocSettlCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(736);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)AllocSettlCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 736);
			}
			if (IndividualAllocID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(467);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)IndividualAllocID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 467);
			}
			if (AllocQty != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(80);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)AllocQty);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 80);
			}
		}
	}
}
