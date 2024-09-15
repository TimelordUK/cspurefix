using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class EvntGrpNoEvents
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (EventType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(865);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)EventType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 865);
			}
			if (EventDate != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(866);
				storage.WriteChar((byte)'=');
				storage.WriteLocalDateOnly((DateOnly)EventDate);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 866);
			}
			if (EventPx != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(867);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)EventPx);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 867);
			}
			if (EventText != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(868);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)EventText);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 868);
			}
		}
	}
}
