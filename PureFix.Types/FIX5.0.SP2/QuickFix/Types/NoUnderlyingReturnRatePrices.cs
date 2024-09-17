using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingReturnRatePrices : IFixGroup
	{
		[TagDetails(Tag = 43065, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingReturnRatePriceBasis {get; set;}
		
		[TagDetails(Tag = 43066, Type = TagType.Float, Offset = 1, Required = false)]
		public double? UnderlyingReturnRatePrice {get; set;}
		
		[TagDetails(Tag = 43067, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingReturnRatePriceCurrency {get; set;}
		
		[TagDetails(Tag = 43068, Type = TagType.Int, Offset = 3, Required = false)]
		public int? UnderlyingReturnRatePriceType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingReturnRatePriceBasis is not null) writer.WriteWholeNumber(43065, UnderlyingReturnRatePriceBasis.Value);
			if (UnderlyingReturnRatePrice is not null) writer.WriteNumber(43066, UnderlyingReturnRatePrice.Value);
			if (UnderlyingReturnRatePriceCurrency is not null) writer.WriteString(43067, UnderlyingReturnRatePriceCurrency);
			if (UnderlyingReturnRatePriceType is not null) writer.WriteWholeNumber(43068, UnderlyingReturnRatePriceType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingReturnRatePriceBasis = view.GetInt32(43065);
			UnderlyingReturnRatePrice = view.GetDouble(43066);
			UnderlyingReturnRatePriceCurrency = view.GetString(43067);
			UnderlyingReturnRatePriceType = view.GetInt32(43068);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingReturnRatePriceBasis":
					value = UnderlyingReturnRatePriceBasis;
					break;
				case "UnderlyingReturnRatePrice":
					value = UnderlyingReturnRatePrice;
					break;
				case "UnderlyingReturnRatePriceCurrency":
					value = UnderlyingReturnRatePriceCurrency;
					break;
				case "UnderlyingReturnRatePriceType":
					value = UnderlyingReturnRatePriceType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
