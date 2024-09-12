using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class CrossOrderCancelReplaceRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? OrderID { get; set; } // 37 STRING
		public string? CrossID { get; set; } // 548 STRING
		public string? OrigCrossID { get; set; } // 551 STRING
		public int? CrossType { get; set; } // 549 INT
		public int? CrossPrioritization { get; set; } // 550 INT
		public SideCrossOrdModGrp? SideCrossOrdModGrp { get; set; }
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public string? SettlType { get; set; } // 63 CHAR
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public string? HandlInst { get; set; } // 21 CHAR
		public string? ExecInst { get; set; } // 18 MULTIPLEVALUESTRING
		public double? MinQty { get; set; } // 110 QTY
		public double? MaxFloor { get; set; } // 111 QTY
		public string? ExDestination { get; set; } // 100 EXCHANGE
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		public string? ProcessCode { get; set; } // 81 CHAR
		public double? PrevClosePx { get; set; } // 140 PRICE
		public bool? LocateReqd { get; set; } // 114 BOOLEAN
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public Stipulations? Stipulations { get; set; }
		public string? OrdType { get; set; } // 40 CHAR
		public int? PriceType { get; set; } // 423 INT
		public double? Price { get; set; } // 44 PRICE
		public double? StopPx { get; set; } // 99 PRICE
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public string? ComplianceID { get; set; } // 376 STRING
		public string? IOIID { get; set; } // 23 STRING
		public string? QuoteID { get; set; } // 117 STRING
		public string? TimeInForce { get; set; } // 59 CHAR
		public DateTime? EffectiveTime { get; set; } // 168 UTCTIMESTAMP
		public DateTime? ExpireDate { get; set; } // 432 LOCALMKTDATE
		public DateTime? ExpireTime { get; set; } // 126 UTCTIMESTAMP
		public int? GTBookingInst { get; set; } // 427 INT
		public double? MaxShow { get; set; } // 210 QTY
		public PegInstructions? PegInstructions { get; set; }
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		public int? TargetStrategy { get; set; } // 847 INT
		public string? TargetStrategyParameters { get; set; } // 848 STRING
		public double? ParticipationRate { get; set; } // 849 PERCENTAGE
		public string? CancellationRights { get; set; } // 480 CHAR
		public string? MoneyLaunderingStatus { get; set; } // 481 CHAR
		public string? RegistID { get; set; } // 513 STRING
		public string? Designation { get; set; } // 494 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
