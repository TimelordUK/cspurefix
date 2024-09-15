using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PtysSubGrpNoPartySubIDs
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (PartySubID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(523);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)PartySubID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 523);
			}
			if (PartySubIDType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(803);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PartySubIDType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 803);
			}
		}
	}
}
