using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CommissionData
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (Commission != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(12);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)Commission);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 12);
			}
			if (CommType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(13);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CommType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 13);
			}
			if (CommCurrency != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(479);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CommCurrency);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 479);
			}
			if (FundRenewWaiv != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(497);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)FundRenewWaiv);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 497);
			}
		}
	}
}
