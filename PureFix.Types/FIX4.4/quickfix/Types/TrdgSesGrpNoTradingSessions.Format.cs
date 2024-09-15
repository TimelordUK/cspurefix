using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TrdgSesGrpNoTradingSessions
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (TradingSessionID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(336);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TradingSessionID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 336);
			}
			if (TradingSessionSubID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(625);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TradingSessionSubID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 625);
			}
		}
	}
}
