using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoQuoteEntries
	{
		[Component(Offset = 0)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 1)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 2)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 3)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
	}
}
