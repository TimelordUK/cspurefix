using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UnderlyingStipulationsNoUnderlyingStips
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (UnderlyingStipType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(888);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingStipType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 888);
			}
			if (UnderlyingStipValue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(889);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingStipValue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 889);
			}
		}
	}
}
