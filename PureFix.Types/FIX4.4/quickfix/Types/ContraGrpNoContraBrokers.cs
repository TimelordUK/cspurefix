using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ContraGrpNoContraBrokers : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 375, Type = TagType.String, Offset = 0, Required = false)]
		public string? ContraBroker { get; set; }
		
		[TagDetails(Tag = 337, Type = TagType.String, Offset = 1, Required = false)]
		public string? ContraTrader { get; set; }
		
		[TagDetails(Tag = 437, Type = TagType.Float, Offset = 2, Required = false)]
		public double? ContraTradeQty { get; set; }
		
		[TagDetails(Tag = 438, Type = TagType.UtcTimestamp, Offset = 3, Required = false)]
		public DateTime? ContraTradeTime { get; set; }
		
		[TagDetails(Tag = 655, Type = TagType.String, Offset = 4, Required = false)]
		public string? ContraLegRefID { get; set; }
		
	}
}
