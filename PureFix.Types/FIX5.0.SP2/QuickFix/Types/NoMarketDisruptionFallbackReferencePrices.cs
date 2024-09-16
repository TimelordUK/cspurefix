using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoMarketDisruptionFallbackReferencePrices : IFixGroup
	{
		[TagDetails(Tag = 41097, Type = TagType.Int, Offset = 0, Required = false)]
		public int? MarketDisruptionFallbackUnderlierType {get; set;}
		
		[TagDetails(Tag = 41098, Type = TagType.String, Offset = 1, Required = false)]
		public string? MarketDisruptionFallbackUnderlierSecurityID {get; set;}
		
		[TagDetails(Tag = 41099, Type = TagType.String, Offset = 2, Required = false)]
		public string? MarketDisruptionFallbackUnderlierSecurityIDSource {get; set;}
		
		[TagDetails(Tag = 41100, Type = TagType.String, Offset = 3, Required = false)]
		public string? MarketDisruptionFallbackUnderlierSecurityDesc {get; set;}
		
		[TagDetails(Tag = 41101, Type = TagType.Length, Offset = 4, Required = false, LinksToTag = 41102)]
		public int? EncodedMarketDisruptionFallbackUnderlierSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 41102, Type = TagType.RawData, Offset = 5, Required = false, LinksToTag = 41101)]
		public byte[]? EncodedMarketDisruptionFallbackUnderlierSecurityDesc {get; set;}
		
		[TagDetails(Tag = 41103, Type = TagType.Float, Offset = 6, Required = false)]
		public double? MarketDisruptionFallbackOpenUnits {get; set;}
		
		[TagDetails(Tag = 41104, Type = TagType.String, Offset = 7, Required = false)]
		public string? MarketDisruptionFallbackBasketCurrency {get; set;}
		
		[TagDetails(Tag = 41105, Type = TagType.Float, Offset = 8, Required = false)]
		public double? MarketDisruptionFallbackBasketDivisor {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MarketDisruptionFallbackUnderlierType is not null) writer.WriteWholeNumber(41097, MarketDisruptionFallbackUnderlierType.Value);
			if (MarketDisruptionFallbackUnderlierSecurityID is not null) writer.WriteString(41098, MarketDisruptionFallbackUnderlierSecurityID);
			if (MarketDisruptionFallbackUnderlierSecurityIDSource is not null) writer.WriteString(41099, MarketDisruptionFallbackUnderlierSecurityIDSource);
			if (MarketDisruptionFallbackUnderlierSecurityDesc is not null) writer.WriteString(41100, MarketDisruptionFallbackUnderlierSecurityDesc);
			if (EncodedMarketDisruptionFallbackUnderlierSecurityDesc is not null)
			{
				writer.WriteWholeNumber(41101, EncodedMarketDisruptionFallbackUnderlierSecurityDesc.Length);
				writer.WriteBuffer(41102, EncodedMarketDisruptionFallbackUnderlierSecurityDesc);
			}
			if (MarketDisruptionFallbackOpenUnits is not null) writer.WriteNumber(41103, MarketDisruptionFallbackOpenUnits.Value);
			if (MarketDisruptionFallbackBasketCurrency is not null) writer.WriteString(41104, MarketDisruptionFallbackBasketCurrency);
			if (MarketDisruptionFallbackBasketDivisor is not null) writer.WriteNumber(41105, MarketDisruptionFallbackBasketDivisor.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MarketDisruptionFallbackUnderlierType = view.GetInt32(41097);
			MarketDisruptionFallbackUnderlierSecurityID = view.GetString(41098);
			MarketDisruptionFallbackUnderlierSecurityIDSource = view.GetString(41099);
			MarketDisruptionFallbackUnderlierSecurityDesc = view.GetString(41100);
			EncodedMarketDisruptionFallbackUnderlierSecurityDescLen = view.GetInt32(41101);
			EncodedMarketDisruptionFallbackUnderlierSecurityDesc = view.GetByteArray(41102);
			MarketDisruptionFallbackOpenUnits = view.GetDouble(41103);
			MarketDisruptionFallbackBasketCurrency = view.GetString(41104);
			MarketDisruptionFallbackBasketDivisor = view.GetDouble(41105);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MarketDisruptionFallbackUnderlierType":
					value = MarketDisruptionFallbackUnderlierType;
					break;
				case "MarketDisruptionFallbackUnderlierSecurityID":
					value = MarketDisruptionFallbackUnderlierSecurityID;
					break;
				case "MarketDisruptionFallbackUnderlierSecurityIDSource":
					value = MarketDisruptionFallbackUnderlierSecurityIDSource;
					break;
				case "MarketDisruptionFallbackUnderlierSecurityDesc":
					value = MarketDisruptionFallbackUnderlierSecurityDesc;
					break;
				case "EncodedMarketDisruptionFallbackUnderlierSecurityDescLen":
					value = EncodedMarketDisruptionFallbackUnderlierSecurityDescLen;
					break;
				case "EncodedMarketDisruptionFallbackUnderlierSecurityDesc":
					value = EncodedMarketDisruptionFallbackUnderlierSecurityDesc;
					break;
				case "MarketDisruptionFallbackOpenUnits":
					value = MarketDisruptionFallbackOpenUnits;
					break;
				case "MarketDisruptionFallbackBasketCurrency":
					value = MarketDisruptionFallbackBasketCurrency;
					break;
				case "MarketDisruptionFallbackBasketDivisor":
					value = MarketDisruptionFallbackBasketDivisor;
					break;
				default: return false;
			}
			return true;
		}
	}
}
