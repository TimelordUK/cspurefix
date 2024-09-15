using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NstdPtysSubGrpNoNestedPartySubIDs
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (NestedPartySubID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(545);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)NestedPartySubID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 545);
			}
			if (NestedPartySubIDType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(805);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)NestedPartySubIDType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 805);
			}
		}
	}
}
