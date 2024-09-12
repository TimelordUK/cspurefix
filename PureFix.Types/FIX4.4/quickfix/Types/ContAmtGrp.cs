using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class ContAmtGrp
	{
		[Group(NoOfTag = 518, Offset = 0)]
		public NoContAmts[]? NoContAmts { get; set; }
		
	}
}
