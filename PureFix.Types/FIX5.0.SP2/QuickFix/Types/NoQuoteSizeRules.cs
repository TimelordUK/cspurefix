using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoQuoteSizeRules : IFixGroup
	{
		[TagDetails(Tag = 647, Type = TagType.Float, Offset = 0, Required = false)]
		public double? MinBidSize {get; set;}
		
		[TagDetails(Tag = 648, Type = TagType.Float, Offset = 1, Required = false)]
		public double? MinOfferSize {get; set;}
		
		[TagDetails(Tag = 2447, Type = TagType.Boolean, Offset = 2, Required = false)]
		public bool? FastMarketIndicator {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MinBidSize is not null) writer.WriteNumber(647, MinBidSize.Value);
			if (MinOfferSize is not null) writer.WriteNumber(648, MinOfferSize.Value);
			if (FastMarketIndicator is not null) writer.WriteBoolean(2447, FastMarketIndicator.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MinBidSize = view.GetDouble(647);
			MinOfferSize = view.GetDouble(648);
			FastMarketIndicator = view.GetBool(2447);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MinBidSize":
					value = MinBidSize;
					break;
				case "MinOfferSize":
					value = MinOfferSize;
					break;
				case "FastMarketIndicator":
					value = FastMarketIndicator;
					break;
				default: return false;
			}
			return true;
		}
	}
}
