using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedParties
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (NoNestedPartyIDs != null)
			{
				for (var i = 0; i < NoNestedPartyIDs.Length; ++i)
				{
					((IFixEncoder)NoNestedPartyIDs[i])?.Encode(storage, tags, delimiter);
				}
			}
		}
	}
}
