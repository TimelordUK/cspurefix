using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoMiscFees
	{
		[TagDetails(137)]
		public double? MiscFeeAmt { get; set; } // AMT
		
		[TagDetails(138)]
		public string? MiscFeeCurr { get; set; } // CURRENCY
		
		[TagDetails(139)]
		public string? MiscFeeType { get; set; } // CHAR
		
		[TagDetails(891)]
		public int? MiscFeeBasis { get; set; } // INT
		
	}
}
