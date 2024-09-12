using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoPartySubIDs
	{
		public string? PartySubID { get; set; } // 523 STRING
		public int? PartySubIDType { get; set; } // 803 INT
	}
}
