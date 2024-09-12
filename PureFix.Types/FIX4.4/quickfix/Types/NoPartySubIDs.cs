using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPartySubIDs
	{
		[TagDetails(523)]
		public string? PartySubID { get; set; } // STRING
		
		[TagDetails(803)]
		public int? PartySubIDType { get; set; } // INT
		
	}
}
