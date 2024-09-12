using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class Advertisement : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(2)]
		public string? AdvId { get; set; } // STRING
		
		[TagDetails(5)]
		public string? AdvTransType { get; set; } // STRING
		
		[TagDetails(3)]
		public string? AdvRefID { get; set; } // STRING
		
		public Instrument? Instrument { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(4)]
		public string? AdvSide { get; set; } // CHAR
		
		[TagDetails(53)]
		public double? Quantity { get; set; } // QTY
		
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		[TagDetails(44)]
		public double? Price { get; set; } // PRICE
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(149)]
		public string? URLLink { get; set; } // STRING
		
		[TagDetails(30)]
		public string? LastMkt { get; set; } // EXCHANGE
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
