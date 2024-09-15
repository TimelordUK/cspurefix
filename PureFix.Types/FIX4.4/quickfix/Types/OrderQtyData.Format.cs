using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class OrderQtyData
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (OrderQty != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(38);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)OrderQty);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 38);
			}
			if (CashOrderQty != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(152);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)CashOrderQty);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 152);
			}
			if (OrderPercent != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(516);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)OrderPercent);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 516);
			}
			if (RoundingDirection != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(468);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)RoundingDirection);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 468);
			}
			if (RoundingModulus != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(469);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)RoundingModulus);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 469);
			}
		}
	}
}
