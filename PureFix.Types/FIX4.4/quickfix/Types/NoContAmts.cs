using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoContAmts
	{
		public int? ContAmtType { get; set; } // 519 INT
		public double? ContAmtValue { get; set; } // 520 FLOAT
		public string? ContAmtCurr { get; set; } // 521 CURRENCY
	}
}
