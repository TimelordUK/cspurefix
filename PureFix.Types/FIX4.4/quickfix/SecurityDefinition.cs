using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityDefinition : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SecurityReqID { get; set; } // 320 STRING
		public string? SecurityResponseID { get; set; } // 322 STRING
		public int? SecurityResponseType { get; set; } // 323 INT
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public int? ExpirationCycle { get; set; } // 827 INT
		public double? RoundLot { get; set; } // 561 QTY
		public double? MinTradeVol { get; set; } // 562 QTY
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
