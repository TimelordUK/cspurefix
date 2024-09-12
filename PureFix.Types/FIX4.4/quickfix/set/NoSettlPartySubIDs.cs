using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoSettlPartySubIDs
	{
		public string? SettlPartySubID { get; set; } // 785 STRING
		public int? SettlPartySubIDType { get; set; } // 786 INT
	}
}
