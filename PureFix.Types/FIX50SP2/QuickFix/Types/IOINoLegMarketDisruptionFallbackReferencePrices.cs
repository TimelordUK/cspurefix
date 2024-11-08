using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegMarketDisruptionFallbackReferencePrices : IFixGroup
	{
		[TagDetails(Tag = 41472, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegMarketDisruptionFallbackUnderlierType {get; set;}
		
		[TagDetails(Tag = 41473, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegMarketDisruptionFallbackUnderlierSecurityID {get; set;}
		
		[TagDetails(Tag = 41474, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegMarketDisruptionFallbackUnderlierSecurityIDSource {get; set;}
		
		[TagDetails(Tag = 41475, Type = TagType.String, Offset = 3, Required = false)]
		public string? LegMarketDisruptionFallbackUnderlierSecurityDesc {get; set;}
		
		[TagDetails(Tag = 41476, Type = TagType.Length, Offset = 4, Required = false, LinksToTag = 41477)]
		public int? EncodedLegMarketDisruptionFallbackUnderlierSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 41477, Type = TagType.RawData, Offset = 5, Required = false, LinksToTag = 41476)]
		public byte[]? EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc {get; set;}
		
		[TagDetails(Tag = 41478, Type = TagType.Float, Offset = 6, Required = false)]
		public double? LegMarketDisruptionFallbackOpenUnits {get; set;}
		
		[TagDetails(Tag = 41479, Type = TagType.String, Offset = 7, Required = false)]
		public string? LegMarketDisruptionFallbackBasketCurrency {get; set;}
		
		[TagDetails(Tag = 41480, Type = TagType.Float, Offset = 8, Required = false)]
		public double? LegMarketDisruptionFallbackBasketDivisor {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegMarketDisruptionFallbackUnderlierType is not null) writer.WriteWholeNumber(41472, LegMarketDisruptionFallbackUnderlierType.Value);
			if (LegMarketDisruptionFallbackUnderlierSecurityID is not null) writer.WriteString(41473, LegMarketDisruptionFallbackUnderlierSecurityID);
			if (LegMarketDisruptionFallbackUnderlierSecurityIDSource is not null) writer.WriteString(41474, LegMarketDisruptionFallbackUnderlierSecurityIDSource);
			if (LegMarketDisruptionFallbackUnderlierSecurityDesc is not null) writer.WriteString(41475, LegMarketDisruptionFallbackUnderlierSecurityDesc);
			if (EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc is not null)
			{
				writer.WriteWholeNumber(41476, EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc.Length);
				writer.WriteBuffer(41477, EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc);
			}
			if (LegMarketDisruptionFallbackOpenUnits is not null) writer.WriteNumber(41478, LegMarketDisruptionFallbackOpenUnits.Value);
			if (LegMarketDisruptionFallbackBasketCurrency is not null) writer.WriteString(41479, LegMarketDisruptionFallbackBasketCurrency);
			if (LegMarketDisruptionFallbackBasketDivisor is not null) writer.WriteNumber(41480, LegMarketDisruptionFallbackBasketDivisor.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegMarketDisruptionFallbackUnderlierType = view.GetInt32(41472);
			LegMarketDisruptionFallbackUnderlierSecurityID = view.GetString(41473);
			LegMarketDisruptionFallbackUnderlierSecurityIDSource = view.GetString(41474);
			LegMarketDisruptionFallbackUnderlierSecurityDesc = view.GetString(41475);
			EncodedLegMarketDisruptionFallbackUnderlierSecurityDescLen = view.GetInt32(41476);
			EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc = view.GetByteArray(41477);
			LegMarketDisruptionFallbackOpenUnits = view.GetDouble(41478);
			LegMarketDisruptionFallbackBasketCurrency = view.GetString(41479);
			LegMarketDisruptionFallbackBasketDivisor = view.GetDouble(41480);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegMarketDisruptionFallbackUnderlierType":
					value = LegMarketDisruptionFallbackUnderlierType;
					break;
				case "LegMarketDisruptionFallbackUnderlierSecurityID":
					value = LegMarketDisruptionFallbackUnderlierSecurityID;
					break;
				case "LegMarketDisruptionFallbackUnderlierSecurityIDSource":
					value = LegMarketDisruptionFallbackUnderlierSecurityIDSource;
					break;
				case "LegMarketDisruptionFallbackUnderlierSecurityDesc":
					value = LegMarketDisruptionFallbackUnderlierSecurityDesc;
					break;
				case "EncodedLegMarketDisruptionFallbackUnderlierSecurityDescLen":
					value = EncodedLegMarketDisruptionFallbackUnderlierSecurityDescLen;
					break;
				case "EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc":
					value = EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc;
					break;
				case "LegMarketDisruptionFallbackOpenUnits":
					value = LegMarketDisruptionFallbackOpenUnits;
					break;
				case "LegMarketDisruptionFallbackBasketCurrency":
					value = LegMarketDisruptionFallbackBasketCurrency;
					break;
				case "LegMarketDisruptionFallbackBasketDivisor":
					value = LegMarketDisruptionFallbackBasketDivisor;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegMarketDisruptionFallbackUnderlierType = null;
			LegMarketDisruptionFallbackUnderlierSecurityID = null;
			LegMarketDisruptionFallbackUnderlierSecurityIDSource = null;
			LegMarketDisruptionFallbackUnderlierSecurityDesc = null;
			EncodedLegMarketDisruptionFallbackUnderlierSecurityDescLen = null;
			EncodedLegMarketDisruptionFallbackUnderlierSecurityDesc = null;
			LegMarketDisruptionFallbackOpenUnits = null;
			LegMarketDisruptionFallbackBasketCurrency = null;
			LegMarketDisruptionFallbackBasketDivisor = null;
		}
	}
}
