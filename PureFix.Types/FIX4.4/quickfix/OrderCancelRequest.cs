using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class OrderCancelRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? OrigClOrdID { get; set; } // 41 STRING
		public string? OrderID { get; set; } // 37 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public string? ClOrdLinkID { get; set; } // 583 STRING
		public string? ListID { get; set; } // 66 STRING
		public DateTime? OrigOrdModTime { get; set; } // 586 UTCTIMESTAMP
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public Parties? Parties { get; set; }
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public OrderQtyData? OrderQtyData { get; set; }
		public string? ComplianceID { get; set; } // 376 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
