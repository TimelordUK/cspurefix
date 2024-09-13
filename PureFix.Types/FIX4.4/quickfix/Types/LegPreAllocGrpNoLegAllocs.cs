using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class LegPreAllocGrpNoLegAllocs
	{
		[TagDetails(Tag = 671, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegAllocAccount { get; set; }
		
		[TagDetails(Tag = 672, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegIndividualAllocID { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public NestedParties2? NestedParties2 { get; set; }
		
		[TagDetails(Tag = 673, Type = TagType.Float, Offset = 3, Required = false)]
		public double? LegAllocQty { get; set; }
		
		[TagDetails(Tag = 674, Type = TagType.String, Offset = 4, Required = false)]
		public string? LegAllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 675, Type = TagType.String, Offset = 5, Required = false)]
		public string? LegSettlCurrency { get; set; }
		
	}
}
