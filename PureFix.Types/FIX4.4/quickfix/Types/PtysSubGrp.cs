using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class PtysSubGrp
	{
		[Group(NoOfTag = 802, Offset = 0)]
		public NoPartySubIDs[]? NoPartySubIDs { get; set; }
		
	}
}
