using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoMDEntries
	{
		[TagDetails(269, TagType.String)]
		public string? MDEntryType { get; set; }
		
		[TagDetails(270, TagType.Float)]
		public double? MDEntryPx { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(271, TagType.Float)]
		public double? MDEntrySize { get; set; }
		
		[TagDetails(272, TagType.UtcDateOnly)]
		public DateTime? MDEntryDate { get; set; }
		
		[TagDetails(273, TagType.UtcTimeOnly)]
		public DateTime? MDEntryTime { get; set; }
		
		[TagDetails(274, TagType.String)]
		public string? TickDirection { get; set; }
		
		[TagDetails(275, TagType.String)]
		public string? MDMkt { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(276, TagType.String)]
		public string? QuoteCondition { get; set; }
		
		[TagDetails(277, TagType.String)]
		public string? TradeCondition { get; set; }
		
		[TagDetails(282, TagType.String)]
		public string? MDEntryOriginator { get; set; }
		
		[TagDetails(283, TagType.String)]
		public string? LocationID { get; set; }
		
		[TagDetails(284, TagType.String)]
		public string? DeskID { get; set; }
		
		[TagDetails(286, TagType.String)]
		public string? OpenCloseSettlFlag { get; set; }
		
		[TagDetails(59, TagType.String)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(432, TagType.LocalDate)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(126, TagType.UtcTimestamp)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(110, TagType.Float)]
		public double? MinQty { get; set; }
		
		[TagDetails(18, TagType.String)]
		public string? ExecInst { get; set; }
		
		[TagDetails(287, TagType.Int)]
		public int? SellerDays { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(299, TagType.String)]
		public string? QuoteEntryID { get; set; }
		
		[TagDetails(288, TagType.String)]
		public string? MDEntryBuyer { get; set; }
		
		[TagDetails(289, TagType.String)]
		public string? MDEntrySeller { get; set; }
		
		[TagDetails(346, TagType.Int)]
		public int? NumberOfOrders { get; set; }
		
		[TagDetails(290, TagType.Int)]
		public int? MDEntryPositionNo { get; set; }
		
		[TagDetails(546, TagType.String)]
		public string? Scope { get; set; }
		
		[TagDetails(811, TagType.Float)]
		public double? PriceDelta { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
	}
}
