using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ListCancelRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ListID { get; set; } // 66 STRING
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public DateTime? TradeOriginationDate { get; set; } // 229 LOCALMKTDATE
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
