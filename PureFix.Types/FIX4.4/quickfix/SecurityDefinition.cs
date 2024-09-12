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
		[TagDetails(320)]
		public string? SecurityReqID { get; set; } // STRING
		
		[TagDetails(322)]
		public string? SecurityResponseID { get; set; } // STRING
		
		[TagDetails(323)]
		public int? SecurityResponseType { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(827)]
		public int? ExpirationCycle { get; set; } // INT
		
		[TagDetails(561)]
		public double? RoundLot { get; set; } // QTY
		
		[TagDetails(562)]
		public double? MinTradeVol { get; set; } // QTY
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
