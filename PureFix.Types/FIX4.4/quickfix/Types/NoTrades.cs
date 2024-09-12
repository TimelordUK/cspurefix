using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoTrades
	{
		public string? TradeReportID { get; set; } // 571 STRING
		public string? SecondaryTradeReportID { get; set; } // 818 STRING
	}
}
