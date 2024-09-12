using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class SettlInstructionsData
	{
		[TagDetails(172)]
		public int? SettlDeliveryType { get; set; } // INT
		
		[TagDetails(169)]
		public int? StandInstDbType { get; set; } // INT
		
		[TagDetails(170)]
		public string? StandInstDbName { get; set; } // STRING
		
		[TagDetails(171)]
		public string? StandInstDbID { get; set; } // STRING
		
		public DlvyInstGrp? DlvyInstGrp { get; set; }
	}
}
