using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrmtLegSecListGrpNoLegs : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(Tag = 690, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegSwapType { get; set; }
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegSettlType { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public LegStipulations? LegStipulations { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public LegBenchmarkCurveData? LegBenchmarkCurveData { get; set; }
		
	}
}
