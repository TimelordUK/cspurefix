using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoMDEntries
	{
		public string? MDEntryType { get; set; } // 269 CHAR
		public double? MDEntryPx { get; set; } // 270 PRICE
		public string? Currency { get; set; } // 15 CURRENCY
		public double? MDEntrySize { get; set; } // 271 QTY
		public DateTime? MDEntryDate { get; set; } // 272 UTCDATEONLY
		public DateTime? MDEntryTime { get; set; } // 273 UTCTIMEONLY
		public string? TickDirection { get; set; } // 274 CHAR
		public string? MDMkt { get; set; } // 275 EXCHANGE
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? QuoteCondition { get; set; } // 276 MULTIPLEVALUESTRING
		public string? TradeCondition { get; set; } // 277 MULTIPLEVALUESTRING
		public string? MDEntryOriginator { get; set; } // 282 STRING
		public string? LocationID { get; set; } // 283 STRING
		public string? DeskID { get; set; } // 284 STRING
		public string? OpenCloseSettlFlag { get; set; } // 286 MULTIPLEVALUESTRING
		public string? TimeInForce { get; set; } // 59 CHAR
		public DateTime? ExpireDate { get; set; } // 432 LOCALMKTDATE
		public DateTime? ExpireTime { get; set; } // 126 UTCTIMESTAMP
		public double? MinQty { get; set; } // 110 QTY
		public string? ExecInst { get; set; } // 18 MULTIPLEVALUESTRING
		public int? SellerDays { get; set; } // 287 INT
		public string? OrderID { get; set; } // 37 STRING
		public string? QuoteEntryID { get; set; } // 299 STRING
		public string? MDEntryBuyer { get; set; } // 288 STRING
		public string? MDEntrySeller { get; set; } // 289 STRING
		public int? NumberOfOrders { get; set; } // 346 INT
		public int? MDEntryPositionNo { get; set; } // 290 INT
		public string? Scope { get; set; } // 546 MULTIPLEVALUESTRING
		public double? PriceDelta { get; set; } // 811 FLOAT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
	}
}
