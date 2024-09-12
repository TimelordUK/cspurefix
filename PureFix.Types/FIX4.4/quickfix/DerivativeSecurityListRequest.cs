using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class DerivativeSecurityListRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SecurityReqID { get; set; } // 320 STRING
		public int? SecurityListRequestType { get; set; } // 559 INT
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		public string? SecuritySubType { get; set; } // 762 STRING
		public string? Currency { get; set; } // 15 CURRENCY
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
