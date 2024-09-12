using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class Advertisement : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? AdvId { get; set; } // 2 STRING
		public string? AdvTransType { get; set; } // 5 STRING
		public string? AdvRefID { get; set; } // 3 STRING
		public Instrument? Instrument { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? AdvSide { get; set; } // 4 CHAR
		public double? Quantity { get; set; } // 53 QTY
		public int? QtyType { get; set; } // 854 INT
		public double? Price { get; set; } // 44 PRICE
		public string? Currency { get; set; } // 15 CURRENCY
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? URLLink { get; set; } // 149 STRING
		public string? LastMkt { get; set; } // 30 EXCHANGE
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
