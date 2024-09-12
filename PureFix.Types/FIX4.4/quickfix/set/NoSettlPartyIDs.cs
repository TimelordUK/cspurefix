using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoSettlPartyIDs
	{
		public string? SettlPartyID { get; set; } // 782 STRING
		public string? SettlPartyIDSource { get; set; } // 783 CHAR
		public int? SettlPartyRole { get; set; } // 784 INT
		public SettlPtysSubGrp? SettlPtysSubGrp { get; set; }
	}
}
