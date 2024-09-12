using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoLegs
	{
		[Component]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(682, TagType.String)]
		public string? LegIOIQty { get; set; }
		
		[Component]
		public LegStipulations? LegStipulations { get; set; }
		
	}
}
