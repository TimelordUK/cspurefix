using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class DiscretionInstructions
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (DiscretionInst != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(388);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)DiscretionInst);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 388);
			}
			if (DiscretionOffsetValue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(389);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)DiscretionOffsetValue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 389);
			}
			if (DiscretionMoveType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(841);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)DiscretionMoveType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 841);
			}
			if (DiscretionOffsetType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(842);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)DiscretionOffsetType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 842);
			}
			if (DiscretionLimitType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(843);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)DiscretionLimitType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 843);
			}
			if (DiscretionRoundDirection != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(844);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)DiscretionRoundDirection);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 844);
			}
			if (DiscretionScope != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(846);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)DiscretionScope);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 846);
			}
		}
	}
}
