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
		[TagDetails(571)]
		public string? TradeReportID { get; set; } // STRING
		
		[TagDetails(818)]
		public string? SecondaryTradeReportID { get; set; } // STRING
		
	}
}
