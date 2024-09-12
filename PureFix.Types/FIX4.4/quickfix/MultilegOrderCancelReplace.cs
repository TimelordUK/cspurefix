using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MultilegOrderCancelReplace : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? OrderID { get; set; } // 37 STRING
		public string? OrigClOrdID { get; set; } // 41 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public string? ClOrdLinkID { get; set; } // 583 STRING
		public DateTime? OrigOrdModTime { get; set; } // 586 UTCTIMESTAMP
		public Parties? Parties { get; set; }
		public DateTime? TradeOriginationDate { get; set; } // 229 LOCALMKTDATE
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public string? DayBookingInst { get; set; } // 589 CHAR
		public string? BookingUnit { get; set; } // 590 CHAR
		public string? PreallocMethod { get; set; } // 591 CHAR
		public string? AllocID { get; set; } // 70 STRING
		public PreAllocMlegGrp? PreAllocMlegGrp { get; set; }
		public string? SettlType { get; set; } // 63 CHAR
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public string? CashMargin { get; set; } // 544 CHAR
		public string? ClearingFeeIndicator { get; set; } // 635 STRING
		public string? HandlInst { get; set; } // 21 CHAR
		public string? ExecInst { get; set; } // 18 MULTIPLEVALUESTRING
		public double? MinQty { get; set; } // 110 QTY
		public double? MaxFloor { get; set; } // 111 QTY
		public string? ExDestination { get; set; } // 100 EXCHANGE
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		public string? ProcessCode { get; set; } // 81 CHAR
		public string? Side { get; set; } // 54 CHAR
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public double? PrevClosePx { get; set; } // 140 PRICE
		public LegOrdGrp? LegOrdGrp { get; set; }
		public bool? LocateReqd { get; set; } // 114 BOOLEAN
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public int? QtyType { get; set; } // 854 INT
		public OrderQtyData? OrderQtyData { get; set; }
		public string? OrdType { get; set; } // 40 CHAR
		public int? PriceType { get; set; } // 423 INT
		public double? Price { get; set; } // 44 PRICE
		public double? StopPx { get; set; } // 99 PRICE
		public string? Currency { get; set; } // 15 CURRENCY
		public string? ComplianceID { get; set; } // 376 STRING
		public bool? SolicitedFlag { get; set; } // 377 BOOLEAN
		public string? IOIID { get; set; } // 23 STRING
		public string? QuoteID { get; set; } // 117 STRING
		public string? TimeInForce { get; set; } // 59 CHAR
		public DateTime? EffectiveTime { get; set; } // 168 UTCTIMESTAMP
		public DateTime? ExpireDate { get; set; } // 432 LOCALMKTDATE
		public DateTime? ExpireTime { get; set; } // 126 UTCTIMESTAMP
		public int? GTBookingInst { get; set; } // 427 INT
		public CommissionData? CommissionData { get; set; }
		public string? OrderCapacity { get; set; } // 528 CHAR
		public string? OrderRestrictions { get; set; } // 529 MULTIPLEVALUESTRING
		public int? CustOrderCapacity { get; set; } // 582 INT
		public bool? ForexReq { get; set; } // 121 BOOLEAN
		public string? SettlCurrency { get; set; } // 120 CURRENCY
		public int? BookingType { get; set; } // 775 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? PositionEffect { get; set; } // 77 CHAR
		public int? CoveredOrUncovered { get; set; } // 203 INT
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
		public int? MultiLegRptTypeReq { get; set; } // 563 INT
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
