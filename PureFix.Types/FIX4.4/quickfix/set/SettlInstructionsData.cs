using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class SettlInstructionsData
	{
		public int? SettlDeliveryType { get; set; } // 172 INT
		public int? StandInstDbType { get; set; } // 169 INT
		public string? StandInstDbName { get; set; } // 170 STRING
		public string? StandInstDbID { get; set; } // 171 STRING
		public DlvyInstGrp? DlvyInstGrp { get; set; }
	}
}
