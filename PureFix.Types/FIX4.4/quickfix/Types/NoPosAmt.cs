using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPosAmt
	{
		public string? PosAmtType { get; set; } // 707 STRING
		public double? PosAmt { get; set; } // 708 AMT
	}
}
