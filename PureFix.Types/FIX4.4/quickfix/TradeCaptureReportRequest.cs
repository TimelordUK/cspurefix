using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class TradeCaptureReportRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(568)]
		public string? TradeRequestID { get; set; } // STRING
		
		[TagDetails(569)]
		public int? TradeRequestType { get; set; } // INT
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		[TagDetails(571)]
		public string? TradeReportID { get; set; } // STRING
		
		[TagDetails(818)]
		public string? SecondaryTradeReportID { get; set; } // STRING
		
		[TagDetails(17)]
		public string? ExecID { get; set; } // STRING
		
		[TagDetails(150)]
		public string? ExecType { get; set; } // CHAR
		
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(573)]
		public string? MatchStatus { get; set; } // CHAR
		
		[TagDetails(828)]
		public int? TrdType { get; set; } // INT
		
		[TagDetails(829)]
		public int? TrdSubType { get; set; } // INT
		
		[TagDetails(830)]
		public string? TransferReason { get; set; } // STRING
		
		[TagDetails(855)]
		public int? SecondaryTrdType { get; set; } // INT
		
		[TagDetails(820)]
		public string? TradeLinkID { get; set; } // STRING
		
		[TagDetails(880)]
		public string? TrdMatchID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public TrdCapDtGrp? TrdCapDtGrp { get; set; }
		[TagDetails(715)]
		public DateTime? ClearingBusinessDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(943)]
		public string? TimeBracket { get; set; } // STRING
		
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(442)]
		public string? MultiLegReportingType { get; set; } // CHAR
		
		[TagDetails(578)]
		public string? TradeInputSource { get; set; } // STRING
		
		[TagDetails(579)]
		public string? TradeInputDevice { get; set; } // STRING
		
		[TagDetails(725)]
		public int? ResponseTransportType { get; set; } // INT
		
		[TagDetails(726)]
		public string? ResponseDestination { get; set; } // STRING
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
