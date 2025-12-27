using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("l", FixVersion.FIX43)]
	public sealed partial class BidResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 1, Required = false)]
		public string? BidID {get; set;}
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2, Required = false)]
		public string? ClientBidID {get; set;}
		
		public sealed partial class NoBidComponents : IFixGroup
		{
			[Component(Offset = 0, Required = true)]
			public CommissionData? CommissionData {get; set;}
			
			[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = false)]
			public string? ListID {get; set;}
			
			[TagDetails(Tag = 421, Type = TagType.String, Offset = 2, Required = false)]
			public string? Country {get; set;}
			
			[TagDetails(Tag = 54, Type = TagType.String, Offset = 3, Required = false)]
			public string? Side {get; set;}
			
			[TagDetails(Tag = 44, Type = TagType.Float, Offset = 4, Required = false)]
			public double? Price {get; set;}
			
			[TagDetails(Tag = 423, Type = TagType.Int, Offset = 5, Required = false)]
			public int? PriceType {get; set;}
			
			[TagDetails(Tag = 406, Type = TagType.Float, Offset = 6, Required = false)]
			public double? FairValue {get; set;}
			
			[TagDetails(Tag = 430, Type = TagType.Int, Offset = 7, Required = false)]
			public int? NetGrossInd {get; set;}
			
			[TagDetails(Tag = 63, Type = TagType.String, Offset = 8, Required = false)]
			public string? SettlmntTyp {get; set;}
			
			[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 9, Required = false)]
			public DateOnly? FutSettDate {get; set;}
			
			[TagDetails(Tag = 336, Type = TagType.String, Offset = 10, Required = false)]
			public string? TradingSessionID {get; set;}
			
			[TagDetails(Tag = 625, Type = TagType.String, Offset = 11, Required = false)]
			public string? TradingSessionSubID {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 12, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 13, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 14, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
				if (ListID is not null) writer.WriteString(66, ListID);
				if (Country is not null) writer.WriteString(421, Country);
				if (Side is not null) writer.WriteString(54, Side);
				if (Price is not null) writer.WriteNumber(44, Price.Value);
				if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
				if (FairValue is not null) writer.WriteNumber(406, FairValue.Value);
				if (NetGrossInd is not null) writer.WriteWholeNumber(430, NetGrossInd.Value);
				if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
				if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
				if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				if (view.GetView("CommissionData") is IMessageView viewCommissionData)
				{
					CommissionData = new();
					((IFixParser)CommissionData).Parse(viewCommissionData);
				}
				ListID = view.GetString(66);
				Country = view.GetString(421);
				Side = view.GetString(54);
				Price = view.GetDouble(44);
				PriceType = view.GetInt32(423);
				FairValue = view.GetDouble(406);
				NetGrossInd = view.GetInt32(430);
				SettlmntTyp = view.GetString(63);
				FutSettDate = view.GetDateOnly(64);
				TradingSessionID = view.GetString(336);
				TradingSessionSubID = view.GetString(625);
				Text = view.GetString(58);
				EncodedTextLen = view.GetInt32(354);
				EncodedText = view.GetByteArray(355);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "CommissionData":
					{
						value = CommissionData;
						break;
					}
					case "ListID":
					{
						value = ListID;
						break;
					}
					case "Country":
					{
						value = Country;
						break;
					}
					case "Side":
					{
						value = Side;
						break;
					}
					case "Price":
					{
						value = Price;
						break;
					}
					case "PriceType":
					{
						value = PriceType;
						break;
					}
					case "FairValue":
					{
						value = FairValue;
						break;
					}
					case "NetGrossInd":
					{
						value = NetGrossInd;
						break;
					}
					case "SettlmntTyp":
					{
						value = SettlmntTyp;
						break;
					}
					case "FutSettDate":
					{
						value = FutSettDate;
						break;
					}
					case "TradingSessionID":
					{
						value = TradingSessionID;
						break;
					}
					case "TradingSessionSubID":
					{
						value = TradingSessionSubID;
						break;
					}
					case "Text":
					{
						value = Text;
						break;
					}
					case "EncodedTextLen":
					{
						value = EncodedTextLen;
						break;
					}
					case "EncodedText":
					{
						value = EncodedText;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				((IFixReset?)CommissionData)?.Reset();
				ListID = null;
				Country = null;
				Side = null;
				Price = null;
				PriceType = null;
				FairValue = null;
				NetGrossInd = null;
				SettlmntTyp = null;
				FutSettDate = null;
				TradingSessionID = null;
				TradingSessionSubID = null;
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
			}
		}
		[Group(NoOfTag = 420, Offset = 3, Required = true)]
		public NoBidComponents[]? BidComponents {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (BidID is not null) writer.WriteString(390, BidID);
			if (ClientBidID is not null) writer.WriteString(391, ClientBidID);
			if (BidComponents is not null && BidComponents.Length != 0)
			{
				writer.WriteWholeNumber(420, BidComponents.Length);
				for (int i = 0; i < BidComponents.Length; i++)
				{
					((IFixEncoder)BidComponents[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			BidID = view.GetString(390);
			ClientBidID = view.GetString(391);
			if (view.GetView("NoBidComponents") is IMessageView viewNoBidComponents)
			{
				var count = viewNoBidComponents.GroupCount();
				BidComponents = new NoBidComponents[count];
				for (int i = 0; i < count; i++)
				{
					BidComponents[i] = new();
					((IFixParser)BidComponents[i]).Parse(viewNoBidComponents.GetGroupInstance(i));
				}
			}
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
				{
					value = StandardHeader;
					break;
				}
				case "BidID":
				{
					value = BidID;
					break;
				}
				case "ClientBidID":
				{
					value = ClientBidID;
					break;
				}
				case "NoBidComponents":
				{
					value = BidComponents;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			BidID = null;
			ClientBidID = null;
			BidComponents = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
