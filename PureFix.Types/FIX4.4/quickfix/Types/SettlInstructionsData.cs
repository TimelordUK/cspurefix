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
		[TagDetails(172, TagType.Int)]
		public int? SettlDeliveryType { get; set; }
		
		[TagDetails(169, TagType.Int)]
		public int? StandInstDbType { get; set; }
		
		[TagDetails(170, TagType.String)]
		public string? StandInstDbName { get; set; }
		
		[TagDetails(171, TagType.String)]
		public string? StandInstDbID { get; set; }
		
		[Component]
		public DlvyInstGrp? DlvyInstGrp { get; set; }
		
	}
}
