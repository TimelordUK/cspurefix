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
		[TagDetails(Tag = 375, Type = TagType.String, Offset = 0)]
		public string? ContraBroker { get; set; }
		
		[TagDetails(Tag = 337, Type = TagType.String, Offset = 1)]
		public string? ContraTrader { get; set; }
		
		[TagDetails(Tag = 437, Type = TagType.Float, Offset = 2)]
		public double? ContraTradeQty { get; set; }
		
		[TagDetails(Tag = 438, Type = TagType.UtcTimestamp, Offset = 3)]
		public DateTime? ContraTradeTime { get; set; }
		
		[TagDetails(Tag = 655, Type = TagType.String, Offset = 4)]
		public string? ContraLegRefID { get; set; }
		
	}
}
