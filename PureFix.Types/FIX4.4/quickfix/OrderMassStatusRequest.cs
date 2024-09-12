using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class OrderMassStatusRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? MassStatusReqID { get; set; } // 584 STRING
		public int? MassStatusReqType { get; set; } // 585 INT
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public Instrument? Instrument { get; set; }
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
