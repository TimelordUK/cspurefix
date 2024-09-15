using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PtysSubGrp
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (NoPartySubIDs != null)
			{
				for (var i = 0; i < NoPartySubIDs.Length; ++i)
				{
					((IFixEncoder)NoPartySubIDs[i])?.Encode(storage, tags, delimiter);
				}
			}
		}
	}
}
