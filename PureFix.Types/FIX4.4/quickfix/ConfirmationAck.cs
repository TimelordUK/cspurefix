using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class ConfirmationAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ConfirmID { get; set; } // 664 STRING
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public int? AffirmStatus { get; set; } // 940 INT
		public int? ConfirmRejReason { get; set; } // 774 INT
		public string? MatchStatus { get; set; } // 573 CHAR
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
