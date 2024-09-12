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
		public string? PartyID { get; set; } // 448 STRING
		public string? PartyIDSource { get; set; } // 447 CHAR
		public int? PartyRole { get; set; } // 452 INT
		public PtysSubGrp? PtysSubGrp { get; set; }
	}
}
