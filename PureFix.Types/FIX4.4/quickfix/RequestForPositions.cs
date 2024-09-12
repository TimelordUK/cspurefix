using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class RequestForPositions : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? PosReqID { get; set; } // 710 STRING
		public int? PosReqType { get; set; } // 724 INT
		public string? MatchStatus { get; set; } // 573 CHAR
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public Instrument? Instrument { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public DateTime? ClearingBusinessDate { get; set; } // 715 LOCALMKTDATE
		public string? SettlSessID { get; set; } // 716 STRING
		public string? SettlSessSubID { get; set; } // 717 STRING
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public int? ResponseTransportType { get; set; } // 725 INT
		public string? ResponseDestination { get; set; } // 726 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
