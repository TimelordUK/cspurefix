using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class StandardTrailer
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (SignatureLength != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(93);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)SignatureLength);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 93);
			}
			if (Signature != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(89);
				storage.WriteChar((byte)'=');
				storage.WriteBuffer((byte[])Signature);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 89);
			}
			if (CheckSum != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(10);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)CheckSum);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 10);
			}
		}
	}
}
