using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class MDFullGrpNoMDEntries
	{
		[TagDetails(Tag = 269, Type = TagType.String, Offset = 0, Required = true)]
		public string? MDEntryType { get; set; }
		
		[TagDetails(Tag = 270, Type = TagType.Float, Offset = 1, Required = false)]
		public double? MDEntryPx { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 2, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 271, Type = TagType.Float, Offset = 3, Required = false)]
		public double? MDEntrySize { get; set; }
		
		[TagDetails(Tag = 272, Type = TagType.UtcDateOnly, Offset = 4, Required = false)]
		public DateOnly? MDEntryDate { get; set; }
		
		[TagDetails(Tag = 273, Type = TagType.UtcTimeOnly, Offset = 5, Required = false)]
		public DateTime? MDEntryTime { get; set; }
		
		[TagDetails(Tag = 274, Type = TagType.String, Offset = 6, Required = false)]
		public string? TickDirection { get; set; }
		
		[TagDetails(Tag = 275, Type = TagType.String, Offset = 7, Required = false)]
		public string? MDMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 276, Type = TagType.String, Offset = 10, Required = false)]
		public string? QuoteCondition { get; set; }
		
		[TagDetails(Tag = 277, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradeCondition { get; set; }
		
		[TagDetails(Tag = 282, Type = TagType.String, Offset = 12, Required = false)]
		public string? MDEntryOriginator { get; set; }
		
		[TagDetails(Tag = 283, Type = TagType.String, Offset = 13, Required = false)]
		public string? LocationID { get; set; }
		
		[TagDetails(Tag = 284, Type = TagType.String, Offset = 14, Required = false)]
		public string? DeskID { get; set; }
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 15, Required = false)]
		public string? OpenCloseSettlFlag { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 16, Required = false)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 18, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 19, Required = false)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 20, Required = false)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 287, Type = TagType.Int, Offset = 21, Required = false)]
		public int? SellerDays { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 22, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 23, Required = false)]
		public string? QuoteEntryID { get; set; }
		
		[TagDetails(Tag = 288, Type = TagType.String, Offset = 24, Required = false)]
		public string? MDEntryBuyer { get; set; }
		
		[TagDetails(Tag = 289, Type = TagType.String, Offset = 25, Required = false)]
		public string? MDEntrySeller { get; set; }
		
		[TagDetails(Tag = 346, Type = TagType.Int, Offset = 26, Required = false)]
		public int? NumberOfOrders { get; set; }
		
		[TagDetails(Tag = 290, Type = TagType.Int, Offset = 27, Required = false)]
		public int? MDEntryPositionNo { get; set; }
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 28, Required = false)]
		public string? Scope { get; set; }
		
		[TagDetails(Tag = 811, Type = TagType.Float, Offset = 29, Required = false)]
		public double? PriceDelta { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 30, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 31, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 32, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
	}
}
