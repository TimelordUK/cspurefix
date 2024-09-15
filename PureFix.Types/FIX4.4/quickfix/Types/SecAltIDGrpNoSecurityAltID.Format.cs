using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecAltIDGrpNoSecurityAltID
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (SecurityAltID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(455);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecurityAltID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 455);
			}
			if (SecurityAltIDSource != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(456);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SecurityAltIDSource);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 456);
			}
		}
	}
}
