using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSettlPartySubIDs
	{
		[TagDetails(785)]
		public string? SettlPartySubID { get; set; } // STRING
		
		[TagDetails(786)]
		public int? SettlPartySubIDType { get; set; } // INT
		
	}
}
