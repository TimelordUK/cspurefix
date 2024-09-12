using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityTypes : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SecurityReqID { get; set; } // 320 STRING
		public string? SecurityResponseID { get; set; } // 322 STRING
		public int? SecurityResponseType { get; set; } // 323 INT
		public int? TotNoSecurityTypes { get; set; } // 557 INT
		public bool? LastFragment { get; set; } // 893 BOOLEAN
		public SecTypesGrp? SecTypesGrp { get; set; }
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
