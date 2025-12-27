using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("W", FixVersion.FIX43)]
	public sealed partial class MarketDataSnapshotFullRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = false)]
		public string? MDReqID {get; set;}
		
		[Component(Offset = 2, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 3, Required = false)]
		public string? FinancialStatus {get; set;}
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 4, Required = false)]
		public string? CorporateAction {get; set;}
		
		[TagDetails(Tag = 387, Type = TagType.Float, Offset = 5, Required = false)]
		public double? TotalVolumeTraded {get; set;}
		
		[TagDetails(Tag = 449, Type = TagType.String, Offset = 6, Required = false)]
		public string? TotalVolumeTradedDate {get; set;}
		
		[TagDetails(Tag = 450, Type = TagType.UtcTimeOnly, Offset = 7, Required = false)]
		public TimeOnly? TotalVolumeTradedTime {get; set;}
		
		[TagDetails(Tag = 451, Type = TagType.Float, Offset = 8, Required = false)]
		public double? NetChgPrevDay {get; set;}
		
		public sealed partial class NoMDEntries : IFixGroup
		{
			[TagDetails(Tag = 269, Type = TagType.String, Offset = 0, Required = true)]
			public string? MDEntryType {get; set;}
			
			[TagDetails(Tag = 270, Type = TagType.Float, Offset = 1, Required = false)]
			public double? MDEntryPx {get; set;}
			
			[TagDetails(Tag = 15, Type = TagType.String, Offset = 2, Required = false)]
			public string? Currency {get; set;}
			
			[TagDetails(Tag = 271, Type = TagType.Float, Offset = 3, Required = false)]
			public double? MDEntrySize {get; set;}
			
			[TagDetails(Tag = 272, Type = TagType.String, Offset = 4, Required = false)]
			public string? MDEntryDate {get; set;}
			
			[TagDetails(Tag = 273, Type = TagType.UtcTimeOnly, Offset = 5, Required = false)]
			public TimeOnly? MDEntryTime {get; set;}
			
			[TagDetails(Tag = 274, Type = TagType.String, Offset = 6, Required = false)]
			public string? TickDirection {get; set;}
			
			[TagDetails(Tag = 275, Type = TagType.String, Offset = 7, Required = false)]
			public string? MDMkt {get; set;}
			
			[TagDetails(Tag = 336, Type = TagType.String, Offset = 8, Required = false)]
			public string? TradingSessionID {get; set;}
			
			[TagDetails(Tag = 625, Type = TagType.String, Offset = 9, Required = false)]
			public string? TradingSessionSubID {get; set;}
			
			[TagDetails(Tag = 276, Type = TagType.String, Offset = 10, Required = false)]
			public string? QuoteCondition {get; set;}
			
			[TagDetails(Tag = 277, Type = TagType.String, Offset = 11, Required = false)]
			public string? TradeCondition {get; set;}
			
			[TagDetails(Tag = 282, Type = TagType.String, Offset = 12, Required = false)]
			public string? MDEntryOriginator {get; set;}
			
			[TagDetails(Tag = 283, Type = TagType.String, Offset = 13, Required = false)]
			public string? LocationID {get; set;}
			
			[TagDetails(Tag = 284, Type = TagType.String, Offset = 14, Required = false)]
			public string? DeskID {get; set;}
			
			[TagDetails(Tag = 286, Type = TagType.String, Offset = 15, Required = false)]
			public string? OpenCloseSettleFlag {get; set;}
			
			[TagDetails(Tag = 59, Type = TagType.String, Offset = 16, Required = false)]
			public string? TimeInForce {get; set;}
			
			[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 17, Required = false)]
			public DateOnly? ExpireDate {get; set;}
			
			[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 18, Required = false)]
			public DateTime? ExpireTime {get; set;}
			
			[TagDetails(Tag = 110, Type = TagType.Float, Offset = 19, Required = false)]
			public double? MinQty {get; set;}
			
			[TagDetails(Tag = 18, Type = TagType.String, Offset = 20, Required = false)]
			public string? ExecInst {get; set;}
			
			[TagDetails(Tag = 287, Type = TagType.Int, Offset = 21, Required = false)]
			public int? SellerDays {get; set;}
			
			[TagDetails(Tag = 37, Type = TagType.String, Offset = 22, Required = false)]
			public string? OrderID {get; set;}
			
			[TagDetails(Tag = 299, Type = TagType.String, Offset = 23, Required = false)]
			public string? QuoteEntryID {get; set;}
			
			[TagDetails(Tag = 288, Type = TagType.String, Offset = 24, Required = false)]
			public string? MDEntryBuyer {get; set;}
			
			[TagDetails(Tag = 289, Type = TagType.String, Offset = 25, Required = false)]
			public string? MDEntrySeller {get; set;}
			
			[TagDetails(Tag = 346, Type = TagType.Int, Offset = 26, Required = false)]
			public int? NumberOfOrders {get; set;}
			
			[TagDetails(Tag = 290, Type = TagType.Int, Offset = 27, Required = false)]
			public int? MDEntryPositionNo {get; set;}
			
			[TagDetails(Tag = 546, Type = TagType.String, Offset = 28, Required = false)]
			public string? Scope {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 29, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 30, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 31, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (MDEntryType is not null) writer.WriteString(269, MDEntryType);
				if (MDEntryPx is not null) writer.WriteNumber(270, MDEntryPx.Value);
				if (Currency is not null) writer.WriteString(15, Currency);
				if (MDEntrySize is not null) writer.WriteNumber(271, MDEntrySize.Value);
				if (MDEntryDate is not null) writer.WriteString(272, MDEntryDate);
				if (MDEntryTime is not null) writer.WriteTimeOnly(273, MDEntryTime.Value);
				if (TickDirection is not null) writer.WriteString(274, TickDirection);
				if (MDMkt is not null) writer.WriteString(275, MDMkt);
				if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
				if (QuoteCondition is not null) writer.WriteString(276, QuoteCondition);
				if (TradeCondition is not null) writer.WriteString(277, TradeCondition);
				if (MDEntryOriginator is not null) writer.WriteString(282, MDEntryOriginator);
				if (LocationID is not null) writer.WriteString(283, LocationID);
				if (DeskID is not null) writer.WriteString(284, DeskID);
				if (OpenCloseSettleFlag is not null) writer.WriteString(286, OpenCloseSettleFlag);
				if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
				if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
				if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
				if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
				if (ExecInst is not null) writer.WriteString(18, ExecInst);
				if (SellerDays is not null) writer.WriteWholeNumber(287, SellerDays.Value);
				if (OrderID is not null) writer.WriteString(37, OrderID);
				if (QuoteEntryID is not null) writer.WriteString(299, QuoteEntryID);
				if (MDEntryBuyer is not null) writer.WriteString(288, MDEntryBuyer);
				if (MDEntrySeller is not null) writer.WriteString(289, MDEntrySeller);
				if (NumberOfOrders is not null) writer.WriteWholeNumber(346, NumberOfOrders.Value);
				if (MDEntryPositionNo is not null) writer.WriteWholeNumber(290, MDEntryPositionNo.Value);
				if (Scope is not null) writer.WriteString(546, Scope);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				MDEntryType = view.GetString(269);
				MDEntryPx = view.GetDouble(270);
				Currency = view.GetString(15);
				MDEntrySize = view.GetDouble(271);
				MDEntryDate = view.GetString(272);
				MDEntryTime = view.GetTimeOnly(273);
				TickDirection = view.GetString(274);
				MDMkt = view.GetString(275);
				TradingSessionID = view.GetString(336);
				TradingSessionSubID = view.GetString(625);
				QuoteCondition = view.GetString(276);
				TradeCondition = view.GetString(277);
				MDEntryOriginator = view.GetString(282);
				LocationID = view.GetString(283);
				DeskID = view.GetString(284);
				OpenCloseSettleFlag = view.GetString(286);
				TimeInForce = view.GetString(59);
				ExpireDate = view.GetDateOnly(432);
				ExpireTime = view.GetDateTime(126);
				MinQty = view.GetDouble(110);
				ExecInst = view.GetString(18);
				SellerDays = view.GetInt32(287);
				OrderID = view.GetString(37);
				QuoteEntryID = view.GetString(299);
				MDEntryBuyer = view.GetString(288);
				MDEntrySeller = view.GetString(289);
				NumberOfOrders = view.GetInt32(346);
				MDEntryPositionNo = view.GetInt32(290);
				Scope = view.GetString(546);
				Text = view.GetString(58);
				EncodedTextLen = view.GetInt32(354);
				EncodedText = view.GetByteArray(355);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "MDEntryType":
					{
						value = MDEntryType;
						break;
					}
					case "MDEntryPx":
					{
						value = MDEntryPx;
						break;
					}
					case "Currency":
					{
						value = Currency;
						break;
					}
					case "MDEntrySize":
					{
						value = MDEntrySize;
						break;
					}
					case "MDEntryDate":
					{
						value = MDEntryDate;
						break;
					}
					case "MDEntryTime":
					{
						value = MDEntryTime;
						break;
					}
					case "TickDirection":
					{
						value = TickDirection;
						break;
					}
					case "MDMkt":
					{
						value = MDMkt;
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
					case "QuoteCondition":
					{
						value = QuoteCondition;
						break;
					}
					case "TradeCondition":
					{
						value = TradeCondition;
						break;
					}
					case "MDEntryOriginator":
					{
						value = MDEntryOriginator;
						break;
					}
					case "LocationID":
					{
						value = LocationID;
						break;
					}
					case "DeskID":
					{
						value = DeskID;
						break;
					}
					case "OpenCloseSettleFlag":
					{
						value = OpenCloseSettleFlag;
						break;
					}
					case "TimeInForce":
					{
						value = TimeInForce;
						break;
					}
					case "ExpireDate":
					{
						value = ExpireDate;
						break;
					}
					case "ExpireTime":
					{
						value = ExpireTime;
						break;
					}
					case "MinQty":
					{
						value = MinQty;
						break;
					}
					case "ExecInst":
					{
						value = ExecInst;
						break;
					}
					case "SellerDays":
					{
						value = SellerDays;
						break;
					}
					case "OrderID":
					{
						value = OrderID;
						break;
					}
					case "QuoteEntryID":
					{
						value = QuoteEntryID;
						break;
					}
					case "MDEntryBuyer":
					{
						value = MDEntryBuyer;
						break;
					}
					case "MDEntrySeller":
					{
						value = MDEntrySeller;
						break;
					}
					case "NumberOfOrders":
					{
						value = NumberOfOrders;
						break;
					}
					case "MDEntryPositionNo":
					{
						value = MDEntryPositionNo;
						break;
					}
					case "Scope":
					{
						value = Scope;
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
				MDEntryType = null;
				MDEntryPx = null;
				Currency = null;
				MDEntrySize = null;
				MDEntryDate = null;
				MDEntryTime = null;
				TickDirection = null;
				MDMkt = null;
				TradingSessionID = null;
				TradingSessionSubID = null;
				QuoteCondition = null;
				TradeCondition = null;
				MDEntryOriginator = null;
				LocationID = null;
				DeskID = null;
				OpenCloseSettleFlag = null;
				TimeInForce = null;
				ExpireDate = null;
				ExpireTime = null;
				MinQty = null;
				ExecInst = null;
				SellerDays = null;
				OrderID = null;
				QuoteEntryID = null;
				MDEntryBuyer = null;
				MDEntrySeller = null;
				NumberOfOrders = null;
				MDEntryPositionNo = null;
				Scope = null;
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
			}
		}
		[Group(NoOfTag = 268, Offset = 9, Required = true)]
		public NoMDEntries[]? MDEntries {get; set;}
		
		[Component(Offset = 10, Required = true)]
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
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
			if (TotalVolumeTraded is not null) writer.WriteNumber(387, TotalVolumeTraded.Value);
			if (TotalVolumeTradedDate is not null) writer.WriteString(449, TotalVolumeTradedDate);
			if (TotalVolumeTradedTime is not null) writer.WriteTimeOnly(450, TotalVolumeTradedTime.Value);
			if (NetChgPrevDay is not null) writer.WriteNumber(451, NetChgPrevDay.Value);
			if (MDEntries is not null && MDEntries.Length != 0)
			{
				writer.WriteWholeNumber(268, MDEntries.Length);
				for (int i = 0; i < MDEntries.Length; i++)
				{
					((IFixEncoder)MDEntries[i]).Encode(writer);
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
			MDReqID = view.GetString(262);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			FinancialStatus = view.GetString(291);
			CorporateAction = view.GetString(292);
			TotalVolumeTraded = view.GetDouble(387);
			TotalVolumeTradedDate = view.GetString(449);
			TotalVolumeTradedTime = view.GetTimeOnly(450);
			NetChgPrevDay = view.GetDouble(451);
			if (view.GetView("NoMDEntries") is IMessageView viewNoMDEntries)
			{
				var count = viewNoMDEntries.GroupCount();
				MDEntries = new NoMDEntries[count];
				for (int i = 0; i < count; i++)
				{
					MDEntries[i] = new();
					((IFixParser)MDEntries[i]).Parse(viewNoMDEntries.GetGroupInstance(i));
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
				case "MDReqID":
				{
					value = MDReqID;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "FinancialStatus":
				{
					value = FinancialStatus;
					break;
				}
				case "CorporateAction":
				{
					value = CorporateAction;
					break;
				}
				case "TotalVolumeTraded":
				{
					value = TotalVolumeTraded;
					break;
				}
				case "TotalVolumeTradedDate":
				{
					value = TotalVolumeTradedDate;
					break;
				}
				case "TotalVolumeTradedTime":
				{
					value = TotalVolumeTradedTime;
					break;
				}
				case "NetChgPrevDay":
				{
					value = NetChgPrevDay;
					break;
				}
				case "NoMDEntries":
				{
					value = MDEntries;
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
			MDReqID = null;
			((IFixReset?)Instrument)?.Reset();
			FinancialStatus = null;
			CorporateAction = null;
			TotalVolumeTraded = null;
			TotalVolumeTradedDate = null;
			TotalVolumeTradedTime = null;
			NetChgPrevDay = null;
			MDEntries = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
