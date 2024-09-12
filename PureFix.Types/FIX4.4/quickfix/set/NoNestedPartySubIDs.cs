using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoNestedPartySubIDs
	{
		public string? NestedPartySubID { get; set; } // 545 STRING
		public int? NestedPartySubIDType { get; set; } // 805 INT
	}
}
