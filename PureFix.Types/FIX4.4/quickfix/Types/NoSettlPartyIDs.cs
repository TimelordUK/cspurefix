using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSettlPartyIDs
	{
		[TagDetails(782)]
		public string? SettlPartyID { get; set; } // STRING
		
		[TagDetails(783)]
		public string? SettlPartyIDSource { get; set; } // CHAR
		
		[TagDetails(784)]
		public int? SettlPartyRole { get; set; } // INT
		
		public SettlPtysSubGrp? SettlPtysSubGrp { get; set; }
	}
}
