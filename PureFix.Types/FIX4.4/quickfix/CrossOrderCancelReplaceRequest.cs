using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class CrossOrderCancelReplaceRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(548)]
		public string? CrossID { get; set; } // STRING
		
		[TagDetails(551)]
		public string? OrigCrossID { get; set; } // STRING
		
		[TagDetails(549)]
		public int? CrossType { get; set; } // INT
		
		[TagDetails(550)]
		public int? CrossPrioritization { get; set; } // INT
		
		public SideCrossOrdModGrp? SideCrossOrdModGrp { get; set; }
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(63)]
		public string? SettlType { get; set; } // CHAR
		
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(21)]
		public string? HandlInst { get; set; } // CHAR
		
		[TagDetails(18)]
		public string? ExecInst { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(110)]
		public double? MinQty { get; set; } // QTY
		
		[TagDetails(111)]
		public double? MaxFloor { get; set; } // QTY
		
		[TagDetails(100)]
		public string? ExDestination { get; set; } // EXCHANGE
		
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		[TagDetails(81)]
		public string? ProcessCode { get; set; } // CHAR
		
		[TagDetails(140)]
		public double? PrevClosePx { get; set; } // PRICE
		
		[TagDetails(114)]
		public bool? LocateReqd { get; set; } // BOOLEAN
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public Stipulations? Stipulations { get; set; }
		[TagDetails(40)]
		public string? OrdType { get; set; } // CHAR
		
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		[TagDetails(44)]
		public double? Price { get; set; } // PRICE
		
		[TagDetails(99)]
		public double? StopPx { get; set; } // PRICE
		
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(376)]
		public string? ComplianceID { get; set; } // STRING
		
		[TagDetails(23)]
		public string? IOIID { get; set; } // STRING
		
		[TagDetails(117)]
		public string? QuoteID { get; set; } // STRING
		
		[TagDetails(59)]
		public string? TimeInForce { get; set; } // CHAR
		
		[TagDetails(168)]
		public DateTime? EffectiveTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(432)]
		public DateTime? ExpireDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(126)]
		public DateTime? ExpireTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(427)]
		public int? GTBookingInst { get; set; } // INT
		
		[TagDetails(210)]
		public double? MaxShow { get; set; } // QTY
		
		public PegInstructions? PegInstructions { get; set; }
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		[TagDetails(847)]
		public int? TargetStrategy { get; set; } // INT
		
		[TagDetails(848)]
		public string? TargetStrategyParameters { get; set; } // STRING
		
		[TagDetails(849)]
		public double? ParticipationRate { get; set; } // PERCENTAGE
		
		[TagDetails(480)]
		public string? CancellationRights { get; set; } // CHAR
		
		[TagDetails(481)]
		public string? MoneyLaunderingStatus { get; set; } // CHAR
		
		[TagDetails(513)]
		public string? RegistID { get; set; } // STRING
		
		[TagDetails(494)]
		public string? Designation { get; set; } // STRING
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
