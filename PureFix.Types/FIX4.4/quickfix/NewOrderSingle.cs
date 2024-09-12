using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class NewOrderSingle : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(526)]
		public string? SecondaryClOrdID { get; set; } // STRING
		
		[TagDetails(583)]
		public string? ClOrdLinkID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(229)]
		public DateTime? TradeOriginationDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(589)]
		public string? DayBookingInst { get; set; } // CHAR
		
		[TagDetails(590)]
		public string? BookingUnit { get; set; } // CHAR
		
		[TagDetails(591)]
		public string? PreallocMethod { get; set; } // CHAR
		
		[TagDetails(70)]
		public string? AllocID { get; set; } // STRING
		
		public PreAllocGrp? PreAllocGrp { get; set; }
		[TagDetails(63)]
		public string? SettlType { get; set; } // CHAR
		
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(544)]
		public string? CashMargin { get; set; } // CHAR
		
		[TagDetails(635)]
		public string? ClearingFeeIndicator { get; set; } // STRING
		
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
		
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(140)]
		public double? PrevClosePx { get; set; } // PRICE
		
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(114)]
		public bool? LocateReqd { get; set; } // BOOLEAN
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public Stipulations? Stipulations { get; set; }
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		public OrderQtyData? OrderQtyData { get; set; }
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
		
		[TagDetails(377)]
		public bool? SolicitedFlag { get; set; } // BOOLEAN
		
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
		
		public CommissionData? CommissionData { get; set; }
		[TagDetails(528)]
		public string? OrderCapacity { get; set; } // CHAR
		
		[TagDetails(529)]
		public string? OrderRestrictions { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(582)]
		public int? CustOrderCapacity { get; set; } // INT
		
		[TagDetails(121)]
		public bool? ForexReq { get; set; } // BOOLEAN
		
		[TagDetails(120)]
		public string? SettlCurrency { get; set; } // CURRENCY
		
		[TagDetails(775)]
		public int? BookingType { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(193)]
		public DateTime? SettlDate2 { get; set; } // LOCALMKTDATE
		
		[TagDetails(192)]
		public double? OrderQty2 { get; set; } // QTY
		
		[TagDetails(640)]
		public double? Price2 { get; set; } // PRICE
		
		[TagDetails(77)]
		public string? PositionEffect { get; set; } // CHAR
		
		[TagDetails(203)]
		public int? CoveredOrUncovered { get; set; } // INT
		
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
