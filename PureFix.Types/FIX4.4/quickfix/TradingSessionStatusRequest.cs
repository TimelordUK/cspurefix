using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class TradingSessionStatusRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(335)]
		public string? TradSesReqID { get; set; } // STRING
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(338)]
		public int? TradSesMethod { get; set; } // INT
		
		[TagDetails(339)]
		public int? TradSesMode { get; set; } // INT
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
