using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TrdCollGrpNoTrades : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 0, Required = false)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecondaryTradeReportID { get; set; }
		
	}
}
