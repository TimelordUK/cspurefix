using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AllocationInstructionNoTradeAllocAmts : IFixGroup
	{
		[TagDetails(Tag = 1845, Type = TagType.String, Offset = 0, Required = false)]
		public string? TradeAllocAmtType {get; set;}
		
		[TagDetails(Tag = 1846, Type = TagType.Float, Offset = 1, Required = false)]
		public double? TradeAllocAmt {get; set;}
		
		[TagDetails(Tag = 1847, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradeAllocCurrency {get; set;}
		
		[TagDetails(Tag = 2933, Type = TagType.String, Offset = 3, Required = false)]
		public string? TradeAllocCurrencyCodeSource {get; set;}
		
		[TagDetails(Tag = 1850, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TradeAllocAmtReason {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TradeAllocAmtType is not null) writer.WriteString(1845, TradeAllocAmtType);
			if (TradeAllocAmt is not null) writer.WriteNumber(1846, TradeAllocAmt.Value);
			if (TradeAllocCurrency is not null) writer.WriteString(1847, TradeAllocCurrency);
			if (TradeAllocCurrencyCodeSource is not null) writer.WriteString(2933, TradeAllocCurrencyCodeSource);
			if (TradeAllocAmtReason is not null) writer.WriteWholeNumber(1850, TradeAllocAmtReason.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TradeAllocAmtType = view.GetString(1845);
			TradeAllocAmt = view.GetDouble(1846);
			TradeAllocCurrency = view.GetString(1847);
			TradeAllocCurrencyCodeSource = view.GetString(2933);
			TradeAllocAmtReason = view.GetInt32(1850);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TradeAllocAmtType":
					value = TradeAllocAmtType;
					break;
				case "TradeAllocAmt":
					value = TradeAllocAmt;
					break;
				case "TradeAllocCurrency":
					value = TradeAllocCurrency;
					break;
				case "TradeAllocCurrencyCodeSource":
					value = TradeAllocCurrencyCodeSource;
					break;
				case "TradeAllocAmtReason":
					value = TradeAllocAmtReason;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TradeAllocAmtType = null;
			TradeAllocAmt = null;
			TradeAllocCurrency = null;
			TradeAllocCurrencyCodeSource = null;
			TradeAllocAmtReason = null;
		}
	}
}
