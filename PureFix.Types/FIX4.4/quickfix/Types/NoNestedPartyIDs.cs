using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoNestedPartyIDs
	{
		public string? NestedPartyID { get; set; } // 524 STRING
		public string? NestedPartyIDSource { get; set; } // 525 CHAR
		public int? NestedPartyRole { get; set; } // 538 INT
		public NstdPtysSubGrp? NstdPtysSubGrp { get; set; }
	}
}
