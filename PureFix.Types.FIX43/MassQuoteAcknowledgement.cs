using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("b", FixVersion.FIX43)]
	public sealed partial class MassQuoteAcknowledgement : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = false)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 297, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteStatus {get; set;}
		
		[TagDetails(Tag = 300, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRejectReason {get; set;}
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteResponseLevel {get; set;}
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 6, Required = false)]
		public int? QuoteType {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public Parties? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 10, Required = false)]
		public string? Text {get; set;}
		
		public sealed partial class NoQuoteSets : IFixGroup
		{
			[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = false)]
			public string? QuoteSetID {get; set;}
			
			[Component(Offset = 1, Required = false)]
			public UnderlyingInstrument? UnderlyingInstrument {get; set;}
			
			[TagDetails(Tag = 304, Type = TagType.Int, Offset = 2, Required = false)]
			public int? TotQuoteEntries {get; set;}
			
			public sealed partial class NoQuoteEntries : IFixGroup
			{
				[TagDetails(Tag = 299, Type = TagType.String, Offset = 0, Required = false)]
				public string? QuoteEntryID {get; set;}
				
				[Component(Offset = 1, Required = false)]
				public Instrument? Instrument {get; set;}
				
				[TagDetails(Tag = 132, Type = TagType.Float, Offset = 2, Required = false)]
				public double? BidPx {get; set;}
				
				[TagDetails(Tag = 133, Type = TagType.Float, Offset = 3, Required = false)]
				public double? OfferPx {get; set;}
				
				[TagDetails(Tag = 134, Type = TagType.Float, Offset = 4, Required = false)]
				public double? BidSize {get; set;}
				
				[TagDetails(Tag = 135, Type = TagType.Float, Offset = 5, Required = false)]
				public double? OfferSize {get; set;}
				
				[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 6, Required = false)]
				public DateTime? ValidUntilTime {get; set;}
				
				[TagDetails(Tag = 188, Type = TagType.Float, Offset = 7, Required = false)]
				public double? BidSpotRate {get; set;}
				
				[TagDetails(Tag = 190, Type = TagType.Float, Offset = 8, Required = false)]
				public double? OfferSpotRate {get; set;}
				
				[TagDetails(Tag = 189, Type = TagType.Float, Offset = 9, Required = false)]
				public double? BidForwardPoints {get; set;}
				
				[TagDetails(Tag = 191, Type = TagType.Float, Offset = 10, Required = false)]
				public double? OfferForwardPoints {get; set;}
				
				[TagDetails(Tag = 631, Type = TagType.Float, Offset = 11, Required = false)]
				public double? MidPx {get; set;}
				
				[TagDetails(Tag = 632, Type = TagType.Float, Offset = 12, Required = false)]
				public double? BidYield {get; set;}
				
				[TagDetails(Tag = 633, Type = TagType.Float, Offset = 13, Required = false)]
				public double? MidYield {get; set;}
				
				[TagDetails(Tag = 634, Type = TagType.Float, Offset = 14, Required = false)]
				public double? OfferYield {get; set;}
				
				[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 15, Required = false)]
				public DateTime? TransactTime {get; set;}
				
				[TagDetails(Tag = 336, Type = TagType.String, Offset = 16, Required = false)]
				public string? TradingSessionID {get; set;}
				
				[TagDetails(Tag = 625, Type = TagType.String, Offset = 17, Required = false)]
				public string? TradingSessionSubID {get; set;}
				
				[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 18, Required = false)]
				public DateOnly? FutSettDate {get; set;}
				
				[TagDetails(Tag = 40, Type = TagType.String, Offset = 19, Required = false)]
				public string? OrdType {get; set;}
				
				[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 20, Required = false)]
				public DateOnly? FutSettDate2 {get; set;}
				
				[TagDetails(Tag = 192, Type = TagType.Float, Offset = 21, Required = false)]
				public double? OrderQty2 {get; set;}
				
				[TagDetails(Tag = 642, Type = TagType.Float, Offset = 22, Required = false)]
				public double? BidForwardPoints2 {get; set;}
				
				[TagDetails(Tag = 643, Type = TagType.Float, Offset = 23, Required = false)]
				public double? OfferForwardPoints2 {get; set;}
				
				[TagDetails(Tag = 15, Type = TagType.String, Offset = 24, Required = false)]
				public string? Currency {get; set;}
				
				[TagDetails(Tag = 368, Type = TagType.Int, Offset = 25, Required = false)]
				public int? QuoteEntryRejectReason {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (QuoteEntryID is not null) writer.WriteString(299, QuoteEntryID);
					if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
					if (BidPx is not null) writer.WriteNumber(132, BidPx.Value);
					if (OfferPx is not null) writer.WriteNumber(133, OfferPx.Value);
					if (BidSize is not null) writer.WriteNumber(134, BidSize.Value);
					if (OfferSize is not null) writer.WriteNumber(135, OfferSize.Value);
					if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
					if (BidSpotRate is not null) writer.WriteNumber(188, BidSpotRate.Value);
					if (OfferSpotRate is not null) writer.WriteNumber(190, OfferSpotRate.Value);
					if (BidForwardPoints is not null) writer.WriteNumber(189, BidForwardPoints.Value);
					if (OfferForwardPoints is not null) writer.WriteNumber(191, OfferForwardPoints.Value);
					if (MidPx is not null) writer.WriteNumber(631, MidPx.Value);
					if (BidYield is not null) writer.WriteNumber(632, BidYield.Value);
					if (MidYield is not null) writer.WriteNumber(633, MidYield.Value);
					if (OfferYield is not null) writer.WriteNumber(634, OfferYield.Value);
					if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
					if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
					if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
					if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
					if (OrdType is not null) writer.WriteString(40, OrdType);
					if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
					if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
					if (BidForwardPoints2 is not null) writer.WriteNumber(642, BidForwardPoints2.Value);
					if (OfferForwardPoints2 is not null) writer.WriteNumber(643, OfferForwardPoints2.Value);
					if (Currency is not null) writer.WriteString(15, Currency);
					if (QuoteEntryRejectReason is not null) writer.WriteWholeNumber(368, QuoteEntryRejectReason.Value);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					QuoteEntryID = view.GetString(299);
					if (view.GetView("Instrument") is IMessageView viewInstrument)
					{
						Instrument = new();
						((IFixParser)Instrument).Parse(viewInstrument);
					}
					BidPx = view.GetDouble(132);
					OfferPx = view.GetDouble(133);
					BidSize = view.GetDouble(134);
					OfferSize = view.GetDouble(135);
					ValidUntilTime = view.GetDateTime(62);
					BidSpotRate = view.GetDouble(188);
					OfferSpotRate = view.GetDouble(190);
					BidForwardPoints = view.GetDouble(189);
					OfferForwardPoints = view.GetDouble(191);
					MidPx = view.GetDouble(631);
					BidYield = view.GetDouble(632);
					MidYield = view.GetDouble(633);
					OfferYield = view.GetDouble(634);
					TransactTime = view.GetDateTime(60);
					TradingSessionID = view.GetString(336);
					TradingSessionSubID = view.GetString(625);
					FutSettDate = view.GetDateOnly(64);
					OrdType = view.GetString(40);
					FutSettDate2 = view.GetDateOnly(193);
					OrderQty2 = view.GetDouble(192);
					BidForwardPoints2 = view.GetDouble(642);
					OfferForwardPoints2 = view.GetDouble(643);
					Currency = view.GetString(15);
					QuoteEntryRejectReason = view.GetInt32(368);
				}
				
				bool IFixLookup.TryGetByTag(string name, out object? value)
				{
					value = null;
					switch (name)
					{
						case "QuoteEntryID":
						{
							value = QuoteEntryID;
							break;
						}
						case "Instrument":
						{
							value = Instrument;
							break;
						}
						case "BidPx":
						{
							value = BidPx;
							break;
						}
						case "OfferPx":
						{
							value = OfferPx;
							break;
						}
						case "BidSize":
						{
							value = BidSize;
							break;
						}
						case "OfferSize":
						{
							value = OfferSize;
							break;
						}
						case "ValidUntilTime":
						{
							value = ValidUntilTime;
							break;
						}
						case "BidSpotRate":
						{
							value = BidSpotRate;
							break;
						}
						case "OfferSpotRate":
						{
							value = OfferSpotRate;
							break;
						}
						case "BidForwardPoints":
						{
							value = BidForwardPoints;
							break;
						}
						case "OfferForwardPoints":
						{
							value = OfferForwardPoints;
							break;
						}
						case "MidPx":
						{
							value = MidPx;
							break;
						}
						case "BidYield":
						{
							value = BidYield;
							break;
						}
						case "MidYield":
						{
							value = MidYield;
							break;
						}
						case "OfferYield":
						{
							value = OfferYield;
							break;
						}
						case "TransactTime":
						{
							value = TransactTime;
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
						case "FutSettDate":
						{
							value = FutSettDate;
							break;
						}
						case "OrdType":
						{
							value = OrdType;
							break;
						}
						case "FutSettDate2":
						{
							value = FutSettDate2;
							break;
						}
						case "OrderQty2":
						{
							value = OrderQty2;
							break;
						}
						case "BidForwardPoints2":
						{
							value = BidForwardPoints2;
							break;
						}
						case "OfferForwardPoints2":
						{
							value = OfferForwardPoints2;
							break;
						}
						case "Currency":
						{
							value = Currency;
							break;
						}
						case "QuoteEntryRejectReason":
						{
							value = QuoteEntryRejectReason;
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
					QuoteEntryID = null;
					((IFixReset?)Instrument)?.Reset();
					BidPx = null;
					OfferPx = null;
					BidSize = null;
					OfferSize = null;
					ValidUntilTime = null;
					BidSpotRate = null;
					OfferSpotRate = null;
					BidForwardPoints = null;
					OfferForwardPoints = null;
					MidPx = null;
					BidYield = null;
					MidYield = null;
					OfferYield = null;
					TransactTime = null;
					TradingSessionID = null;
					TradingSessionSubID = null;
					FutSettDate = null;
					OrdType = null;
					FutSettDate2 = null;
					OrderQty2 = null;
					BidForwardPoints2 = null;
					OfferForwardPoints2 = null;
					Currency = null;
					QuoteEntryRejectReason = null;
				}
			}
			[Group(NoOfTag = 295, Offset = 3, Required = false)]
			public NoQuoteEntries[]? QuoteEntries {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (QuoteSetID is not null) writer.WriteString(302, QuoteSetID);
				if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
				if (TotQuoteEntries is not null) writer.WriteWholeNumber(304, TotQuoteEntries.Value);
				if (QuoteEntries is not null && QuoteEntries.Length != 0)
				{
					writer.WriteWholeNumber(295, QuoteEntries.Length);
					for (int i = 0; i < QuoteEntries.Length; i++)
					{
						((IFixEncoder)QuoteEntries[i]).Encode(writer);
					}
				}
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				QuoteSetID = view.GetString(302);
				if (view.GetView("UnderlyingInstrument") is IMessageView viewUnderlyingInstrument)
				{
					UnderlyingInstrument = new();
					((IFixParser)UnderlyingInstrument).Parse(viewUnderlyingInstrument);
				}
				TotQuoteEntries = view.GetInt32(304);
				if (view.GetView("NoQuoteEntries") is IMessageView viewNoQuoteEntries)
				{
					var count = viewNoQuoteEntries.GroupCount();
					QuoteEntries = new NoQuoteEntries[count];
					for (int i = 0; i < count; i++)
					{
						QuoteEntries[i] = new();
						((IFixParser)QuoteEntries[i]).Parse(viewNoQuoteEntries.GetGroupInstance(i));
					}
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "QuoteSetID":
					{
						value = QuoteSetID;
						break;
					}
					case "UnderlyingInstrument":
					{
						value = UnderlyingInstrument;
						break;
					}
					case "TotQuoteEntries":
					{
						value = TotQuoteEntries;
						break;
					}
					case "NoQuoteEntries":
					{
						value = QuoteEntries;
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
				QuoteSetID = null;
				((IFixReset?)UnderlyingInstrument)?.Reset();
				TotQuoteEntries = null;
				QuoteEntries = null;
			}
		}
		[Group(NoOfTag = 296, Offset = 11, Required = false)]
		public NoQuoteSets[]? QuoteSets {get; set;}
		
		[Component(Offset = 12, Required = true)]
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
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteStatus is not null) writer.WriteWholeNumber(297, QuoteStatus.Value);
			if (QuoteRejectReason is not null) writer.WriteWholeNumber(300, QuoteRejectReason.Value);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (QuoteSets is not null && QuoteSets.Length != 0)
			{
				writer.WriteWholeNumber(296, QuoteSets.Length);
				for (int i = 0; i < QuoteSets.Length; i++)
				{
					((IFixEncoder)QuoteSets[i]).Encode(writer);
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
			QuoteReqID = view.GetString(131);
			QuoteID = view.GetString(117);
			QuoteStatus = view.GetInt32(297);
			QuoteRejectReason = view.GetInt32(300);
			QuoteResponseLevel = view.GetInt32(301);
			QuoteType = view.GetInt32(537);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			Account = view.GetString(1);
			AccountType = view.GetInt32(581);
			Text = view.GetString(58);
			if (view.GetView("NoQuoteSets") is IMessageView viewNoQuoteSets)
			{
				var count = viewNoQuoteSets.GroupCount();
				QuoteSets = new NoQuoteSets[count];
				for (int i = 0; i < count; i++)
				{
					QuoteSets[i] = new();
					((IFixParser)QuoteSets[i]).Parse(viewNoQuoteSets.GetGroupInstance(i));
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
				case "QuoteReqID":
				{
					value = QuoteReqID;
					break;
				}
				case "QuoteID":
				{
					value = QuoteID;
					break;
				}
				case "QuoteStatus":
				{
					value = QuoteStatus;
					break;
				}
				case "QuoteRejectReason":
				{
					value = QuoteRejectReason;
					break;
				}
				case "QuoteResponseLevel":
				{
					value = QuoteResponseLevel;
					break;
				}
				case "QuoteType":
				{
					value = QuoteType;
					break;
				}
				case "Parties":
				{
					value = Parties;
					break;
				}
				case "Account":
				{
					value = Account;
					break;
				}
				case "AccountType":
				{
					value = AccountType;
					break;
				}
				case "Text":
				{
					value = Text;
					break;
				}
				case "NoQuoteSets":
				{
					value = QuoteSets;
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
			QuoteReqID = null;
			QuoteID = null;
			QuoteStatus = null;
			QuoteRejectReason = null;
			QuoteResponseLevel = null;
			QuoteType = null;
			((IFixReset?)Parties)?.Reset();
			Account = null;
			AccountType = null;
			Text = null;
			QuoteSets = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
