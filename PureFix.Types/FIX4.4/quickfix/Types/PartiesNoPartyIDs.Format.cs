using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PartiesNoPartyIDs
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (PartyID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(448);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)PartyID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 448);
			}
			if (PartyIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(447);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)PartyIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 447);
			}
			if (PartyRole != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(452);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PartyRole);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 452);
			}
		}
	}
}
