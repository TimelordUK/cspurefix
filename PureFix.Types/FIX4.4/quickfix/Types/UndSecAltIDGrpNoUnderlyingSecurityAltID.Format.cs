using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UndSecAltIDGrpNoUnderlyingSecurityAltID
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (UnderlyingSecurityAltID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(458);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecurityAltID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 458);
			}
			if (UnderlyingSecurityAltIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(459);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)UnderlyingSecurityAltIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 459);
			}
		}
	}
}
