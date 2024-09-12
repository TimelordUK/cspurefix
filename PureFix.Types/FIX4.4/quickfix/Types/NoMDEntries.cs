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
		[TagDetails(Tag = 269, Type = TagType.String, Offset = 0)]
		public string? MDEntryType { get; set; }
		
		[TagDetails(Tag = 270, Type = TagType.Float, Offset = 1)]
		public double? MDEntryPx { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 2)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 271, Type = TagType.Float, Offset = 3)]
		public double? MDEntrySize { get; set; }
		
		[TagDetails(Tag = 272, Type = TagType.UtcDateOnly, Offset = 4)]
		public DateTime? MDEntryDate { get; set; }
		
		[TagDetails(Tag = 273, Type = TagType.UtcTimeOnly, Offset = 5)]
		public DateTime? MDEntryTime { get; set; }
		
		[TagDetails(Tag = 274, Type = TagType.String, Offset = 6)]
		public string? TickDirection { get; set; }
		
		[TagDetails(Tag = 275, Type = TagType.String, Offset = 7)]
		public string? MDMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 276, Type = TagType.String, Offset = 10)]
		public string? QuoteCondition { get; set; }
		
		[TagDetails(Tag = 277, Type = TagType.String, Offset = 11)]
		public string? TradeCondition { get; set; }
		
		[TagDetails(Tag = 282, Type = TagType.String, Offset = 12)]
		public string? MDEntryOriginator { get; set; }
		
		[TagDetails(Tag = 283, Type = TagType.String, Offset = 13)]
		public string? LocationID { get; set; }
		
		[TagDetails(Tag = 284, Type = TagType.String, Offset = 14)]
		public string? DeskID { get; set; }
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 15)]
		public string? OpenCloseSettlFlag { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 16)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 17)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 18)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 19)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 20)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 287, Type = TagType.Int, Offset = 21)]
		public int? SellerDays { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 22)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 23)]
		public string? QuoteEntryID { get; set; }
		
		[TagDetails(Tag = 288, Type = TagType.String, Offset = 24)]
		public string? MDEntryBuyer { get; set; }
		
		[TagDetails(Tag = 289, Type = TagType.String, Offset = 25)]
		public string? MDEntrySeller { get; set; }
		
		[TagDetails(Tag = 346, Type = TagType.Int, Offset = 26)]
		public int? NumberOfOrders { get; set; }
		
		[TagDetails(Tag = 290, Type = TagType.Int, Offset = 27)]
		public int? MDEntryPositionNo { get; set; }
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 28)]
		public string? Scope { get; set; }
		
		[TagDetails(Tag = 811, Type = TagType.Float, Offset = 29)]
		public double? PriceDelta { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 30)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 31)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 32)]
		public byte[]? EncodedText { get; set; }
		
	}
}
