using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoExecs
	{
		public double? LastQty { get; set; } // 32 QTY
		public string? ExecID { get; set; } // 17 STRING
		public string? SecondaryExecID { get; set; } // 527 STRING
		public double? LastPx { get; set; } // 31 PRICE
		public double? LastParPx { get; set; } // 669 PRICE
		public string? LastCapacity { get; set; } // 29 CHAR
	}
}
