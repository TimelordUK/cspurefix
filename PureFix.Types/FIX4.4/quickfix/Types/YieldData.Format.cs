using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class YieldData
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (YieldType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(235);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)YieldType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 235);
			}
			if (Yield != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(236);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)Yield);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 236);
			}
			if (YieldCalcDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(701);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)YieldCalcDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 701);
			}
			if (YieldRedemptionDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(696);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)YieldRedemptionDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 696);
			}
			if (YieldRedemptionPrice != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(697);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)YieldRedemptionPrice);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 697);
			}
			if (YieldRedemptionPriceType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(698);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)YieldRedemptionPriceType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 698);
			}
		}
	}
}
