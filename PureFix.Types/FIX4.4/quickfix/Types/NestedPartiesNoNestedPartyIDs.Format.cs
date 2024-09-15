using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedPartiesNoNestedPartyIDs
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (NestedPartyID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(524);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)NestedPartyID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 524);
			}
			if (NestedPartyIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(525);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)NestedPartyIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 525);
			}
			if (NestedPartyRole != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(538);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)NestedPartyRole);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 538);
			}
		}
	}
}
