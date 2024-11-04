using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingReturnRateFXConversions : IFixGroup
	{
		[TagDetails(Tag = 43031, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingReturnRateFXCurrencySymbol {get; set;}
		
		[TagDetails(Tag = 43032, Type = TagType.Float, Offset = 1, Required = false)]
		public double? UnderlyingReturnRateFXRate {get; set;}
		
		[TagDetails(Tag = 43033, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingReturnRateFXRateCalc {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingReturnRateFXCurrencySymbol is not null) writer.WriteString(43031, UnderlyingReturnRateFXCurrencySymbol);
			if (UnderlyingReturnRateFXRate is not null) writer.WriteNumber(43032, UnderlyingReturnRateFXRate.Value);
			if (UnderlyingReturnRateFXRateCalc is not null) writer.WriteString(43033, UnderlyingReturnRateFXRateCalc);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingReturnRateFXCurrencySymbol = view.GetString(43031);
			UnderlyingReturnRateFXRate = view.GetDouble(43032);
			UnderlyingReturnRateFXRateCalc = view.GetString(43033);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingReturnRateFXCurrencySymbol":
					value = UnderlyingReturnRateFXCurrencySymbol;
					break;
				case "UnderlyingReturnRateFXRate":
					value = UnderlyingReturnRateFXRate;
					break;
				case "UnderlyingReturnRateFXRateCalc":
					value = UnderlyingReturnRateFXRateCalc;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingReturnRateFXCurrencySymbol = null;
			UnderlyingReturnRateFXRate = null;
			UnderlyingReturnRateFXRateCalc = null;
		}
	}
}
