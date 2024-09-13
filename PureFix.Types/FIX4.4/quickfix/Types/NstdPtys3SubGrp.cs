using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NstdPtys3SubGrp
	{
		[Group(NoOfTag = 952, Offset = 0, Required = false)]
		public NstdPtys3SubGrpNoNested3PartySubIDs[]? NoNested3PartySubIDs { get; set; }
		
	}
}
