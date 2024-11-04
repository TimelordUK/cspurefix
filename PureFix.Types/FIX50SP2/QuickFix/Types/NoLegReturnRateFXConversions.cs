using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegReturnRateFXConversions : IFixGroup
	{
		[TagDetails(Tag = 42531, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegReturnRateFXCurrencySymbol {get; set;}
		
		[TagDetails(Tag = 42532, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LegReturnRateFXRate {get; set;}
		
		[TagDetails(Tag = 42533, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegReturnRateFXRateCalc {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegReturnRateFXCurrencySymbol is not null) writer.WriteString(42531, LegReturnRateFXCurrencySymbol);
			if (LegReturnRateFXRate is not null) writer.WriteNumber(42532, LegReturnRateFXRate.Value);
			if (LegReturnRateFXRateCalc is not null) writer.WriteString(42533, LegReturnRateFXRateCalc);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegReturnRateFXCurrencySymbol = view.GetString(42531);
			LegReturnRateFXRate = view.GetDouble(42532);
			LegReturnRateFXRateCalc = view.GetString(42533);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegReturnRateFXCurrencySymbol":
					value = LegReturnRateFXCurrencySymbol;
					break;
				case "LegReturnRateFXRate":
					value = LegReturnRateFXRate;
					break;
				case "LegReturnRateFXRateCalc":
					value = LegReturnRateFXRateCalc;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegReturnRateFXCurrencySymbol = null;
			LegReturnRateFXRate = null;
			LegReturnRateFXRateCalc = null;
		}
	}
}
