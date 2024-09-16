using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoTrades : IFixGroup
	{
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 0, Required = false)]
		public string? TradeReportID {get; set;}
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecondaryTradeReportID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (SecondaryTradeReportID is not null) writer.WriteString(818, SecondaryTradeReportID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TradeReportID = view.GetString(571);
			SecondaryTradeReportID = view.GetString(818);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TradeReportID":
					value = TradeReportID;
					break;
				case "SecondaryTradeReportID":
					value = SecondaryTradeReportID;
					break;
				default: return false;
			}
			return true;
		}
	}
}
