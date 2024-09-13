using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class InstrmtLegExecGrpNoLegs
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(Tag = 687, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LegQty { get; set; }
		
		[TagDetails(Tag = 690, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegSwapType { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public LegStipulations? LegStipulations { get; set; }
		
		[TagDetails(Tag = 564, Type = TagType.String, Offset = 4, Required = false)]
		public string? LegPositionEffect { get; set; }
		
		[TagDetails(Tag = 565, Type = TagType.Int, Offset = 5, Required = false)]
		public int? LegCoveredOrUncovered { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(Tag = 654, Type = TagType.String, Offset = 7, Required = false)]
		public string? LegRefID { get; set; }
		
		[TagDetails(Tag = 566, Type = TagType.Float, Offset = 8, Required = false)]
		public double? LegPrice { get; set; }
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 9, Required = false)]
		public string? LegSettlType { get; set; }
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 10, Required = false)]
		public DateTime? LegSettlDate { get; set; }
		
		[TagDetails(Tag = 637, Type = TagType.Float, Offset = 11, Required = false)]
		public double? LegLastPx { get; set; }
		
	}
}
