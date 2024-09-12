using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoContAmts
	{
		[TagDetails(519)]
		public int? ContAmtType { get; set; } // INT
		
		[TagDetails(520)]
		public double? ContAmtValue { get; set; } // FLOAT
		
		[TagDetails(521)]
		public string? ContAmtCurr { get; set; } // CURRENCY
		
	}
}
