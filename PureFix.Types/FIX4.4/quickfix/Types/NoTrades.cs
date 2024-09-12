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
		[TagDetails(571, TagType.String)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(818, TagType.String)]
		public string? SecondaryTradeReportID { get; set; }
		
	}
}
