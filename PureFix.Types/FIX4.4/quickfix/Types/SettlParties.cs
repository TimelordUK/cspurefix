using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class SettlParties
	{
		[Group(NoOfTag = 781, Offset = 0, Required = false)]
		public SettlPartiesNoSettlPartyIDs[]? NoSettlPartyIDs { get; set; }
		
	}
}
