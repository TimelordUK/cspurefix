using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoSideCollateralReinvestments : IFixGroup
	{
		[TagDetails(Tag = 2867, Type = TagType.Int, Offset = 0, Required = false)]
		public int? SideCollateralReinvestmentType {get; set;}
		
		[TagDetails(Tag = 2865, Type = TagType.Float, Offset = 1, Required = false)]
		public double? SideCollateralReinvestmentAmount {get; set;}
		
		[TagDetails(Tag = 2866, Type = TagType.String, Offset = 2, Required = false)]
		public string? SideCollateralReinvestmentCurrency {get; set;}
		
		[TagDetails(Tag = 2932, Type = TagType.String, Offset = 3, Required = false)]
		public string? SideCollateralReinvestmentCurrencyCodeSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SideCollateralReinvestmentType is not null) writer.WriteWholeNumber(2867, SideCollateralReinvestmentType.Value);
			if (SideCollateralReinvestmentAmount is not null) writer.WriteNumber(2865, SideCollateralReinvestmentAmount.Value);
			if (SideCollateralReinvestmentCurrency is not null) writer.WriteString(2866, SideCollateralReinvestmentCurrency);
			if (SideCollateralReinvestmentCurrencyCodeSource is not null) writer.WriteString(2932, SideCollateralReinvestmentCurrencyCodeSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			SideCollateralReinvestmentType = view.GetInt32(2867);
			SideCollateralReinvestmentAmount = view.GetDouble(2865);
			SideCollateralReinvestmentCurrency = view.GetString(2866);
			SideCollateralReinvestmentCurrencyCodeSource = view.GetString(2932);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "SideCollateralReinvestmentType":
					value = SideCollateralReinvestmentType;
					break;
				case "SideCollateralReinvestmentAmount":
					value = SideCollateralReinvestmentAmount;
					break;
				case "SideCollateralReinvestmentCurrency":
					value = SideCollateralReinvestmentCurrency;
					break;
				case "SideCollateralReinvestmentCurrencyCodeSource":
					value = SideCollateralReinvestmentCurrencyCodeSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			SideCollateralReinvestmentType = null;
			SideCollateralReinvestmentAmount = null;
			SideCollateralReinvestmentCurrency = null;
			SideCollateralReinvestmentCurrencyCodeSource = null;
		}
	}
}
