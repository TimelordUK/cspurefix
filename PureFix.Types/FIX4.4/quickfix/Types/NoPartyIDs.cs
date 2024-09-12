using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPartyIDs
	{
		[TagDetails(448)]
		public string? PartyID { get; set; } // STRING
		
		[TagDetails(447)]
		public string? PartyIDSource { get; set; } // CHAR
		
		[TagDetails(452)]
		public int? PartyRole { get; set; } // INT
		
		public PtysSubGrp? PtysSubGrp { get; set; }
	}
}
