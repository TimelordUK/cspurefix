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
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (SecondaryTradeReportID is not null) writer.WriteString(818, SecondaryTradeReportID);
		}
	}
}
