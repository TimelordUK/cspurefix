using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class SecurityStatusRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SecurityStatusReqID { get; set; } // 324 STRING
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
