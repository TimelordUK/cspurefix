using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class QuoteStatusRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? QuoteStatusReqID { get; set; } // 649 STRING
		public string? QuoteID { get; set; } // 117 STRING
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
