using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoCrossLegs : IFixGroup
	{
		[TagDetails(Tag = 654, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegRefID {get; set;}
		
		[TagDetails(Tag = 685, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LegOrderQty {get; set;}
		
		[TagDetails(Tag = 690, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegSwapType {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public LegStipulationsComponent? LegStipulations {get; set;}
		
		[TagDetails(Tag = 1366, Type = TagType.String, Offset = 4, Required = false)]
		public string? LegAllocID {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public LegPreAllocGrpComponent? LegPreAllocGrp {get; set;}
		
		[TagDetails(Tag = 1817, Type = TagType.Int, Offset = 6, Required = false)]
		public int? LegClearingAccountType {get; set;}
		
		[TagDetails(Tag = 564, Type = TagType.String, Offset = 7, Required = false)]
		public string? LegPositionEffect {get; set;}
		
		[TagDetails(Tag = 565, Type = TagType.Int, Offset = 8, Required = false)]
		public int? LegCoveredOrUncovered {get; set;}
		
		[Component(Offset = 9, Required = false)]
		public NestedParties3Component? NestedParties3 {get; set;}
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 10, Required = false)]
		public string? LegSettlType {get; set;}
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 11, Required = false)]
		public DateOnly? LegSettlDate {get; set;}
		
		[TagDetails(Tag = 675, Type = TagType.String, Offset = 12, Required = false)]
		public string? LegSettlCurrency {get; set;}
		
		[TagDetails(Tag = 2900, Type = TagType.String, Offset = 13, Required = false)]
		public string? LegSettlCurrencyCodeSource {get; set;}
		
		[TagDetails(Tag = 1379, Type = TagType.Float, Offset = 14, Required = false)]
		public double? LegVolatility {get; set;}
		
		[TagDetails(Tag = 1381, Type = TagType.Float, Offset = 15, Required = false)]
		public double? LegDividendYield {get; set;}
		
		[TagDetails(Tag = 1383, Type = TagType.Float, Offset = 16, Required = false)]
		public double? LegCurrencyRatio {get; set;}
		
		[TagDetails(Tag = 1384, Type = TagType.String, Offset = 17, Required = false)]
		public string? LegExecInst {get; set;}
		
		[TagDetails(Tag = 1689, Type = TagType.Int, Offset = 18, Required = false)]
		public int? LegShortSaleExemptionReason {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegRefID is not null) writer.WriteString(654, LegRefID);
			if (LegOrderQty is not null) writer.WriteNumber(685, LegOrderQty.Value);
			if (LegSwapType is not null) writer.WriteWholeNumber(690, LegSwapType.Value);
			if (LegStipulations is not null) ((IFixEncoder)LegStipulations).Encode(writer);
			if (LegAllocID is not null) writer.WriteString(1366, LegAllocID);
			if (LegPreAllocGrp is not null) ((IFixEncoder)LegPreAllocGrp).Encode(writer);
			if (LegClearingAccountType is not null) writer.WriteWholeNumber(1817, LegClearingAccountType.Value);
			if (LegPositionEffect is not null) writer.WriteString(564, LegPositionEffect);
			if (LegCoveredOrUncovered is not null) writer.WriteWholeNumber(565, LegCoveredOrUncovered.Value);
			if (NestedParties3 is not null) ((IFixEncoder)NestedParties3).Encode(writer);
			if (LegSettlType is not null) writer.WriteString(587, LegSettlType);
			if (LegSettlDate is not null) writer.WriteLocalDateOnly(588, LegSettlDate.Value);
			if (LegSettlCurrency is not null) writer.WriteString(675, LegSettlCurrency);
			if (LegSettlCurrencyCodeSource is not null) writer.WriteString(2900, LegSettlCurrencyCodeSource);
			if (LegVolatility is not null) writer.WriteNumber(1379, LegVolatility.Value);
			if (LegDividendYield is not null) writer.WriteNumber(1381, LegDividendYield.Value);
			if (LegCurrencyRatio is not null) writer.WriteNumber(1383, LegCurrencyRatio.Value);
			if (LegExecInst is not null) writer.WriteString(1384, LegExecInst);
			if (LegShortSaleExemptionReason is not null) writer.WriteWholeNumber(1689, LegShortSaleExemptionReason.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegRefID = view.GetString(654);
			LegOrderQty = view.GetDouble(685);
			LegSwapType = view.GetInt32(690);
			if (view.GetView("LegStipulations") is IMessageView viewLegStipulations)
			{
				LegStipulations = new();
				((IFixParser)LegStipulations).Parse(viewLegStipulations);
			}
			LegAllocID = view.GetString(1366);
			if (view.GetView("LegPreAllocGrp") is IMessageView viewLegPreAllocGrp)
			{
				LegPreAllocGrp = new();
				((IFixParser)LegPreAllocGrp).Parse(viewLegPreAllocGrp);
			}
			LegClearingAccountType = view.GetInt32(1817);
			LegPositionEffect = view.GetString(564);
			LegCoveredOrUncovered = view.GetInt32(565);
			if (view.GetView("NestedParties3") is IMessageView viewNestedParties3)
			{
				NestedParties3 = new();
				((IFixParser)NestedParties3).Parse(viewNestedParties3);
			}
			LegSettlType = view.GetString(587);
			LegSettlDate = view.GetDateOnly(588);
			LegSettlCurrency = view.GetString(675);
			LegSettlCurrencyCodeSource = view.GetString(2900);
			LegVolatility = view.GetDouble(1379);
			LegDividendYield = view.GetDouble(1381);
			LegCurrencyRatio = view.GetDouble(1383);
			LegExecInst = view.GetString(1384);
			LegShortSaleExemptionReason = view.GetInt32(1689);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegRefID":
					value = LegRefID;
					break;
				case "LegOrderQty":
					value = LegOrderQty;
					break;
				case "LegSwapType":
					value = LegSwapType;
					break;
				case "LegStipulations":
					value = LegStipulations;
					break;
				case "LegAllocID":
					value = LegAllocID;
					break;
				case "LegPreAllocGrp":
					value = LegPreAllocGrp;
					break;
				case "LegClearingAccountType":
					value = LegClearingAccountType;
					break;
				case "LegPositionEffect":
					value = LegPositionEffect;
					break;
				case "LegCoveredOrUncovered":
					value = LegCoveredOrUncovered;
					break;
				case "NestedParties3":
					value = NestedParties3;
					break;
				case "LegSettlType":
					value = LegSettlType;
					break;
				case "LegSettlDate":
					value = LegSettlDate;
					break;
				case "LegSettlCurrency":
					value = LegSettlCurrency;
					break;
				case "LegSettlCurrencyCodeSource":
					value = LegSettlCurrencyCodeSource;
					break;
				case "LegVolatility":
					value = LegVolatility;
					break;
				case "LegDividendYield":
					value = LegDividendYield;
					break;
				case "LegCurrencyRatio":
					value = LegCurrencyRatio;
					break;
				case "LegExecInst":
					value = LegExecInst;
					break;
				case "LegShortSaleExemptionReason":
					value = LegShortSaleExemptionReason;
					break;
				default: return false;
			}
			return true;
		}
	}
}
