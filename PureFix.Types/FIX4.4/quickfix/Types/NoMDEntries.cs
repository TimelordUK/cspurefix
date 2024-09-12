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
		[TagDetails(269)]
		public string? MDEntryType { get; set; } // CHAR
		
		[TagDetails(270)]
		public double? MDEntryPx { get; set; } // PRICE
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(271)]
		public double? MDEntrySize { get; set; } // QTY
		
		[TagDetails(272)]
		public DateTime? MDEntryDate { get; set; } // UTCDATEONLY
		
		[TagDetails(273)]
		public DateTime? MDEntryTime { get; set; } // UTCTIMEONLY
		
		[TagDetails(274)]
		public string? TickDirection { get; set; } // CHAR
		
		[TagDetails(275)]
		public string? MDMkt { get; set; } // EXCHANGE
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(276)]
		public string? QuoteCondition { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(277)]
		public string? TradeCondition { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(282)]
		public string? MDEntryOriginator { get; set; } // STRING
		
		[TagDetails(283)]
		public string? LocationID { get; set; } // STRING
		
		[TagDetails(284)]
		public string? DeskID { get; set; } // STRING
		
		[TagDetails(286)]
		public string? OpenCloseSettlFlag { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(59)]
		public string? TimeInForce { get; set; } // CHAR
		
		[TagDetails(432)]
		public DateTime? ExpireDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(126)]
		public DateTime? ExpireTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(110)]
		public double? MinQty { get; set; } // QTY
		
		[TagDetails(18)]
		public string? ExecInst { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(287)]
		public int? SellerDays { get; set; } // INT
		
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(299)]
		public string? QuoteEntryID { get; set; } // STRING
		
		[TagDetails(288)]
		public string? MDEntryBuyer { get; set; } // STRING
		
		[TagDetails(289)]
		public string? MDEntrySeller { get; set; } // STRING
		
		[TagDetails(346)]
		public int? NumberOfOrders { get; set; } // INT
		
		[TagDetails(290)]
		public int? MDEntryPositionNo { get; set; } // INT
		
		[TagDetails(546)]
		public string? Scope { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(811)]
		public double? PriceDelta { get; set; } // FLOAT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
	}
}
