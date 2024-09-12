using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class SecurityStatus : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SecurityStatusReqID { get; set; } // 324 STRING
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public bool? UnsolicitedIndicator { get; set; } // 325 BOOLEAN
		public int? SecurityTradingStatus { get; set; } // 326 INT
		public string? FinancialStatus { get; set; } // 291 MULTIPLEVALUESTRING
		public string? CorporateAction { get; set; } // 292 MULTIPLEVALUESTRING
		public string? HaltReasonChar { get; set; } // 327 CHAR
		public bool? InViewOfCommon { get; set; } // 328 BOOLEAN
		public bool? DueToRelated { get; set; } // 329 BOOLEAN
		public double? BuyVolume { get; set; } // 330 QTY
		public double? SellVolume { get; set; } // 331 QTY
		public double? HighPx { get; set; } // 332 PRICE
		public double? LowPx { get; set; } // 333 PRICE
		public double? LastPx { get; set; } // 31 PRICE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public int? Adjustment { get; set; } // 334 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
