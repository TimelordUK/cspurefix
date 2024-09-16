using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class MarketDataIncrementalRefreshNoMDEntries : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 279, Type = TagType.String, Offset = 0, Required = true)]
		public string? MDUpdateAction { get; set; }
		
		[TagDetails(Tag = 285, Type = TagType.String, Offset = 1, Required = false)]
		public string? DeleteReason { get; set; }
		
		[TagDetails(Tag = 269, Type = TagType.String, Offset = 2, Required = false)]
		public string? MDEntryType { get; set; }
		
		[TagDetails(Tag = 278, Type = TagType.String, Offset = 3, Required = false)]
		public string? MDEntryID { get; set; }
		
		[TagDetails(Tag = 280, Type = TagType.String, Offset = 4, Required = false)]
		public string? MDEntryRefID { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 6, Required = false)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 7, Required = false)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(Tag = 270, Type = TagType.Float, Offset = 8, Required = false)]
		public double? MDEntryPx { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 9, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 271, Type = TagType.Float, Offset = 10, Required = false)]
		public double? MDEntrySize { get; set; }
		
		[TagDetails(Tag = 272, Type = TagType.String, Offset = 11, Required = false)]
		public string? MDEntryDate { get; set; }
		
		[TagDetails(Tag = 273, Type = TagType.UtcTimeOnly, Offset = 12, Required = false)]
		public TimeOnly? MDEntryTime { get; set; }
		
		[TagDetails(Tag = 274, Type = TagType.String, Offset = 13, Required = false)]
		public string? TickDirection { get; set; }
		
		[TagDetails(Tag = 275, Type = TagType.String, Offset = 14, Required = false)]
		public string? MDMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 16, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 276, Type = TagType.String, Offset = 17, Required = false)]
		public string? QuoteCondition { get; set; }
		
		[TagDetails(Tag = 277, Type = TagType.String, Offset = 18, Required = false)]
		public string? TradeCondition { get; set; }
		
		[TagDetails(Tag = 282, Type = TagType.String, Offset = 19, Required = false)]
		public string? MDEntryOriginator { get; set; }
		
		[TagDetails(Tag = 283, Type = TagType.String, Offset = 20, Required = false)]
		public string? LocationID { get; set; }
		
		[TagDetails(Tag = 284, Type = TagType.String, Offset = 21, Required = false)]
		public string? DeskID { get; set; }
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 22, Required = false)]
		public string? OpenCloseSettleFlag { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 23, Required = false)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 24, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 25, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 26, Required = false)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 27, Required = false)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 287, Type = TagType.Int, Offset = 28, Required = false)]
		public int? SellerDays { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 29, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 30, Required = false)]
		public string? QuoteEntryID { get; set; }
		
		[TagDetails(Tag = 288, Type = TagType.String, Offset = 31, Required = false)]
		public string? MDEntryBuyer { get; set; }
		
		[TagDetails(Tag = 289, Type = TagType.String, Offset = 32, Required = false)]
		public string? MDEntrySeller { get; set; }
		
		[TagDetails(Tag = 346, Type = TagType.Int, Offset = 33, Required = false)]
		public int? NumberOfOrders { get; set; }
		
		[TagDetails(Tag = 290, Type = TagType.Int, Offset = 34, Required = false)]
		public int? MDEntryPositionNo { get; set; }
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 35, Required = false)]
		public string? Scope { get; set; }
		
		[TagDetails(Tag = 387, Type = TagType.Float, Offset = 36, Required = false)]
		public double? TotalVolumeTraded { get; set; }
		
		[TagDetails(Tag = 449, Type = TagType.String, Offset = 37, Required = false)]
		public string? TotalVolumeTradedDate { get; set; }
		
		[TagDetails(Tag = 450, Type = TagType.UtcTimeOnly, Offset = 38, Required = false)]
		public TimeOnly? TotalVolumeTradedTime { get; set; }
		
		[TagDetails(Tag = 451, Type = TagType.Float, Offset = 39, Required = false)]
		public double? NetChgPrevDay { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 40, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 41, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 42, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				MDUpdateAction is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MDUpdateAction is not null) writer.WriteString(279, MDUpdateAction);
			if (DeleteReason is not null) writer.WriteString(285, DeleteReason);
			if (MDEntryType is not null) writer.WriteString(269, MDEntryType);
			if (MDEntryID is not null) writer.WriteString(278, MDEntryID);
			if (MDEntryRefID is not null) writer.WriteString(280, MDEntryRefID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
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
			if (TotalVolumeTraded is not null) writer.WriteNumber(387, TotalVolumeTraded.Value);
			if (TotalVolumeTradedDate is not null) writer.WriteString(449, TotalVolumeTradedDate);
			if (TotalVolumeTradedTime is not null) writer.WriteTimeOnly(450, TotalVolumeTradedTime.Value);
			if (NetChgPrevDay is not null) writer.WriteNumber(451, NetChgPrevDay.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
		}
	}
}
