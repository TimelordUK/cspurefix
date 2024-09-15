using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SettlPtysSubGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 801, Offset = 0, Required = false)]
		public SettlPtysSubGrpNoSettlPartySubIDs[]? NoSettlPartySubIDs { get; set; }
		
	}
}
