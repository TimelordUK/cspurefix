using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class StipulationsNoStipulations
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (StipulationType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(233);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)StipulationType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 233);
			}
			if (StipulationValue != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(234);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)StipulationValue);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 234);
			}
		}
	}
}
