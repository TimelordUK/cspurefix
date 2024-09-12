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
		public string? SettlPartyID { get; set; } // 782 STRING
		public string? SettlPartyIDSource { get; set; } // 783 CHAR
		public int? SettlPartyRole { get; set; } // 784 INT
		public SettlPtysSubGrp? SettlPtysSubGrp { get; set; }
	}
}
