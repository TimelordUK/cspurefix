using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class QuoteRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? QuoteReqID { get; set; } // 131 STRING
		public string? RFQReqID { get; set; } // 644 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public string? OrderCapacity { get; set; } // 528 CHAR
		public QuotReqGrp? QuotReqGrp { get; set; }
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
