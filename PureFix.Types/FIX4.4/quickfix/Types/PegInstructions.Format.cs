using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PegInstructions
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (PegOffsetValue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(211);
				storage.WriteChar((byte)'=');
				storage.WriteNumber((double)PegOffsetValue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 211);
			}
			if (PegMoveType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(835);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PegMoveType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 835);
			}
			if (PegOffsetType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(836);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PegOffsetType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 836);
			}
			if (PegLimitType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(837);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PegLimitType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 837);
			}
			if (PegRoundDirection != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(838);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PegRoundDirection);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 838);
			}
			if (PegScope != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(840);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)PegScope);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 840);
			}
		}
	}
}
