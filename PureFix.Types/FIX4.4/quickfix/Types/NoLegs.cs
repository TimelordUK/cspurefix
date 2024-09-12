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
		public InstrumentLeg? InstrumentLeg { get; set; }
		public string? LegIOIQty { get; set; } // 682 STRING
		public LegStipulations? LegStipulations { get; set; }
	}
}
