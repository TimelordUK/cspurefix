using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class ListExecute : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ListID { get; set; } // 66 STRING
		public string? ClientBidID { get; set; } // 391 STRING
		public string? BidID { get; set; } // 390 STRING
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
