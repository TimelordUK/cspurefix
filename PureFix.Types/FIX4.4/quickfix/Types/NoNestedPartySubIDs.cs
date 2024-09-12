using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoNestedPartySubIDs
	{
		public string? NestedPartySubID { get; set; } // 545 STRING
		public int? NestedPartySubIDType { get; set; } // 805 INT
	}
}
