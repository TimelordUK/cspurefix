using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class HopNoHops
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (HopCompID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(628);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)HopCompID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 628);
			}
			if (HopSendingTime != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(629);
				storage.WriteChar((byte)'=');
				storage.WriteUtcTimeStamp((DateTime)HopSendingTime);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 629);
			}
			if (HopRefID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(630);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)HopRefID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 630);
			}
		}
	}
}
