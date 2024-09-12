using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class OrderCancelReject : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? OrderID { get; set; } // 37 STRING
		public string? SecondaryOrderID { get; set; } // 198 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public string? ClOrdLinkID { get; set; } // 583 STRING
		public string? OrigClOrdID { get; set; } // 41 STRING
		public string? OrdStatus { get; set; } // 39 CHAR
		public bool? WorkingIndicator { get; set; } // 636 BOOLEAN
		public DateTime? OrigOrdModTime { get; set; } // 586 UTCTIMESTAMP
		public string? ListID { get; set; } // 66 STRING
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public DateTime? TradeOriginationDate { get; set; } // 229 LOCALMKTDATE
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? CxlRejResponseTo { get; set; } // 434 CHAR
		public int? CxlRejReason { get; set; } // 102 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
