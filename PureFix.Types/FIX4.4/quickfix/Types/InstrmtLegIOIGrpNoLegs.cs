using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class InstrmtLegIOIGrpNoLegs
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(Tag = 682, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegIOIQty { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public LegStipulations? LegStipulations { get; set; }
		
	}
}
