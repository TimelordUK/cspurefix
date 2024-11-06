using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoReturnRateFXConversions : IFixGroup
	{
		[TagDetails(Tag = 42732, Type = TagType.String, Offset = 0, Required = false)]
		public string? ReturnRateFXCurrencySymbol {get; set;}
		
		[TagDetails(Tag = 42733, Type = TagType.Float, Offset = 1, Required = false)]
		public double? ReturnRateFXRate {get; set;}
		
		[TagDetails(Tag = 42734, Type = TagType.String, Offset = 2, Required = false)]
		public string? ReturnRateFXRateCalc {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ReturnRateFXCurrencySymbol is not null) writer.WriteString(42732, ReturnRateFXCurrencySymbol);
			if (ReturnRateFXRate is not null) writer.WriteNumber(42733, ReturnRateFXRate.Value);
			if (ReturnRateFXRateCalc is not null) writer.WriteString(42734, ReturnRateFXRateCalc);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ReturnRateFXCurrencySymbol = view.GetString(42732);
			ReturnRateFXRate = view.GetDouble(42733);
			ReturnRateFXRateCalc = view.GetString(42734);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ReturnRateFXCurrencySymbol":
					value = ReturnRateFXCurrencySymbol;
					break;
				case "ReturnRateFXRate":
					value = ReturnRateFXRate;
					break;
				case "ReturnRateFXRateCalc":
					value = ReturnRateFXRateCalc;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ReturnRateFXCurrencySymbol = null;
			ReturnRateFXRate = null;
			ReturnRateFXRateCalc = null;
		}
	}
}
