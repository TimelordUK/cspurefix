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
		public string? SettlPartySubID { get; set; } // 785 STRING
		public int? SettlPartySubIDType { get; set; } // 786 INT
	}
}
