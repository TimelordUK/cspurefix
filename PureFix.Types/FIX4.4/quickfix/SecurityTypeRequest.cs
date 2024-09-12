using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class SecurityTypeRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SecurityReqID { get; set; } // 320 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public int? Product { get; set; } // 460 INT
		public string? SecurityType { get; set; } // 167 STRING
		public string? SecuritySubType { get; set; } // 762 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
