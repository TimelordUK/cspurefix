using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed partial class Heartbeat
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			((IFixEncoder)StandardHeader!)?.Encode(storage, tags, delimiter);
			if (TestReqID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(112);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TestReqID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 112);
			}
			((IFixEncoder)StandardTrailer!)?.Encode(storage, tags, delimiter);
		}
	}
}
