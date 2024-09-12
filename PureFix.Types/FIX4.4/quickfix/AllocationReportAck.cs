using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class AllocationReportAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? AllocReportID { get; set; } // 755 STRING
		public string? AllocID { get; set; } // 70 STRING
		public Parties? Parties { get; set; }
		public string? SecondaryAllocID { get; set; } // 793 STRING
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public int? AllocStatus { get; set; } // 87 INT
		public int? AllocRejCode { get; set; } // 88 INT
		public int? AllocReportType { get; set; } // 794 INT
		public int? AllocIntermedReqType { get; set; } // 808 INT
		public string? MatchStatus { get; set; } // 573 CHAR
		public int? Product { get; set; } // 460 INT
		public string? SecurityType { get; set; } // 167 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public AllocAckGrp? AllocAckGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
