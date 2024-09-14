using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class QuotReqLegsGrpNoLegs
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(Tag = 687, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LegQty { get; set; }
		
		[TagDetails(Tag = 690, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegSwapType { get; set; }
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 3, Required = false)]
		public string? LegSettlType { get; set; }
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateOnly? LegSettlDate { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public LegStipulations? LegStipulations { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public LegBenchmarkCurveData? LegBenchmarkCurveData { get; set; }
		
	}
}
