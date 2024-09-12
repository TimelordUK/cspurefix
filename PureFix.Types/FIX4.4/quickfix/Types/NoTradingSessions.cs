using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoTradingSessions
	{
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 0)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 1)]
		public string? TradingSessionSubID { get; set; }
		
	}
}
