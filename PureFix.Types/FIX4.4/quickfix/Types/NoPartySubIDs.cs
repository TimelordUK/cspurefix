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
		public string? PartySubID { get; set; } // 523 STRING
		public int? PartySubIDType { get; set; } // 803 INT
	}
}
