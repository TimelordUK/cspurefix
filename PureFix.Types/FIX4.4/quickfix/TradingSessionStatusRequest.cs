using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class TradingSessionStatusRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? TradSesReqID { get; set; } // 335 STRING
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public int? TradSesMethod { get; set; } // 338 INT
		public int? TradSesMode { get; set; } // 339 INT
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
