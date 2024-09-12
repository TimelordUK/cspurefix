using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class OrderMassCancelRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ClOrdID { get; set; } // 11 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public string? MassCancelRequestType { get; set; } // 530 CHAR
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public Instrument? Instrument { get; set; }
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
