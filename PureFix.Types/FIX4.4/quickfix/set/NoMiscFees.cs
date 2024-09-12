using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoMiscFees
	{
		public double? MiscFeeAmt { get; set; } // 137 AMT
		public string? MiscFeeCurr { get; set; } // 138 CURRENCY
		public string? MiscFeeType { get; set; } // 139 CHAR
		public int? MiscFeeBasis { get; set; } // 891 INT
	}
}
