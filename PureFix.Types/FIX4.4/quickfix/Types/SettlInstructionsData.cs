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
		[TagDetails(Tag = 172, Type = TagType.Int, Offset = 0)]
		public int? SettlDeliveryType { get; set; }
		
		[TagDetails(Tag = 169, Type = TagType.Int, Offset = 1)]
		public int? StandInstDbType { get; set; }
		
		[TagDetails(Tag = 170, Type = TagType.String, Offset = 2)]
		public string? StandInstDbName { get; set; }
		
		[TagDetails(Tag = 171, Type = TagType.String, Offset = 3)]
		public string? StandInstDbID { get; set; }
		
		[Component(Offset = 4)]
		public DlvyInstGrp? DlvyInstGrp { get; set; }
		
	}
}
