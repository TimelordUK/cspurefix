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
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ContraBroker is not null) writer.WriteString(375, ContraBroker);
			if (ContraTrader is not null) writer.WriteString(337, ContraTrader);
			if (ContraTradeQty is not null) writer.WriteNumber(437, ContraTradeQty.Value);
			if (ContraTradeTime is not null) writer.WriteUtcTimeStamp(438, ContraTradeTime.Value);
			if (ContraLegRefID is not null) writer.WriteString(655, ContraLegRefID);
		}
	}
}
