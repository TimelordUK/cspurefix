using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class OrderMassCancelReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ClOrdID { get; set; } // 11 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public string? OrderID { get; set; } // 37 STRING
		public string? SecondaryOrderID { get; set; } // 198 STRING
		public string? MassCancelRequestType { get; set; } // 530 CHAR
		public string? MassCancelResponse { get; set; } // 531 CHAR
		public string? MassCancelRejectReason { get; set; } // 532 CHAR
		public int? TotalAffectedOrders { get; set; } // 533 INT
		public AffectedOrdGrp? AffectedOrdGrp { get; set; }
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public Instrument? Instrument { get; set; }
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
