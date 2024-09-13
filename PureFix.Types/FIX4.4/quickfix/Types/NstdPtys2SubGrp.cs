using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NstdPtys2SubGrp
	{
		[Group(NoOfTag = 806, Offset = 0, Required = false)]
		public NstdPtys2SubGrpNoNested2PartySubIDs[]? NoNested2PartySubIDs { get; set; }
		
	}
}
