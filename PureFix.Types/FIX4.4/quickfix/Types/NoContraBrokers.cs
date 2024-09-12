using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoContraBrokers
	{
		[TagDetails(375, TagType.String)]
		public string? ContraBroker { get; set; }
		
		[TagDetails(337, TagType.String)]
		public string? ContraTrader { get; set; }
		
		[TagDetails(437, TagType.Float)]
		public double? ContraTradeQty { get; set; }
		
		[TagDetails(438, TagType.UtcTimestamp)]
		public DateTime? ContraTradeTime { get; set; }
		
		[TagDetails(655, TagType.String)]
		public string? ContraLegRefID { get; set; }
		
	}
}
