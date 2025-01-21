using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class BaseTradingRulesComponent : IFixComponent
	{
		[Component(Offset = 0, Required = false)]
		public PriceLimitsComponent? PriceLimits {get; set;}
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ExpirationCycle {get; set;}
		
		[TagDetails(Tag = 562, Type = TagType.Float, Offset = 2, Required = false)]
		public double? MinTradeVol {get; set;}
		
		[TagDetails(Tag = 1140, Type = TagType.Float, Offset = 3, Required = false)]
		public double? MaxTradeVol {get; set;}
		
		[TagDetails(Tag = 1143, Type = TagType.Float, Offset = 4, Required = false)]
		public double? MaxPriceVariation {get; set;}
		
		[TagDetails(Tag = 1144, Type = TagType.Int, Offset = 5, Required = false)]
		public int? ImpliedMarketIndicator {get; set;}
		
		[TagDetails(Tag = 1245, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingCurrency {get; set;}
		
		[TagDetails(Tag = 561, Type = TagType.Float, Offset = 7, Required = false)]
		public double? RoundLot {get; set;}
		
		[TagDetails(Tag = 1377, Type = TagType.Int, Offset = 8, Required = false)]
		public int? MultilegModel {get; set;}
		
		[TagDetails(Tag = 1378, Type = TagType.Int, Offset = 9, Required = false)]
		public int? MultilegPriceMethod {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 10, Required = false)]
		public int? PriceType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PriceLimits is not null) ((IFixEncoder)PriceLimits).Encode(writer);
			if (ExpirationCycle is not null) writer.WriteWholeNumber(827, ExpirationCycle.Value);
			if (MinTradeVol is not null) writer.WriteNumber(562, MinTradeVol.Value);
			if (MaxTradeVol is not null) writer.WriteNumber(1140, MaxTradeVol.Value);
			if (MaxPriceVariation is not null) writer.WriteNumber(1143, MaxPriceVariation.Value);
			if (ImpliedMarketIndicator is not null) writer.WriteWholeNumber(1144, ImpliedMarketIndicator.Value);
			if (TradingCurrency is not null) writer.WriteString(1245, TradingCurrency);
			if (RoundLot is not null) writer.WriteNumber(561, RoundLot.Value);
			if (MultilegModel is not null) writer.WriteWholeNumber(1377, MultilegModel.Value);
			if (MultilegPriceMethod is not null) writer.WriteWholeNumber(1378, MultilegPriceMethod.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("PriceLimits") is IMessageView viewPriceLimits)
			{
				PriceLimits = new();
				((IFixParser)PriceLimits).Parse(viewPriceLimits);
			}
			ExpirationCycle = view.GetInt32(827);
			MinTradeVol = view.GetDouble(562);
			MaxTradeVol = view.GetDouble(1140);
			MaxPriceVariation = view.GetDouble(1143);
			ImpliedMarketIndicator = view.GetInt32(1144);
			TradingCurrency = view.GetString(1245);
			RoundLot = view.GetDouble(561);
			MultilegModel = view.GetInt32(1377);
			MultilegPriceMethod = view.GetInt32(1378);
			PriceType = view.GetInt32(423);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PriceLimits":
					value = PriceLimits;
					break;
				case "ExpirationCycle":
					value = ExpirationCycle;
					break;
				case "MinTradeVol":
					value = MinTradeVol;
					break;
				case "MaxTradeVol":
					value = MaxTradeVol;
					break;
				case "MaxPriceVariation":
					value = MaxPriceVariation;
					break;
				case "ImpliedMarketIndicator":
					value = ImpliedMarketIndicator;
					break;
				case "TradingCurrency":
					value = TradingCurrency;
					break;
				case "RoundLot":
					value = RoundLot;
					break;
				case "MultilegModel":
					value = MultilegModel;
					break;
				case "MultilegPriceMethod":
					value = MultilegPriceMethod;
					break;
				case "PriceType":
					value = PriceType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)PriceLimits)?.Reset();
			ExpirationCycle = null;
			MinTradeVol = null;
			MaxTradeVol = null;
			MaxPriceVariation = null;
			ImpliedMarketIndicator = null;
			TradingCurrency = null;
			RoundLot = null;
			MultilegModel = null;
			MultilegPriceMethod = null;
			PriceType = null;
		}
	}
}
