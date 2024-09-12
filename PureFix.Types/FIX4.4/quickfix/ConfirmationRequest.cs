using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class ConfirmationRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ConfirmReqID { get; set; } // 859 STRING
		public int? ConfirmType { get; set; } // 773 INT
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		public string? AllocID { get; set; } // 70 STRING
		public string? SecondaryAllocID { get; set; } // 793 STRING
		public string? IndividualAllocID { get; set; } // 467 STRING
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? AllocAccount { get; set; } // 79 STRING
		public int? AllocAcctIDSource { get; set; } // 661 INT
		public int? AllocAccountType { get; set; } // 798 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
