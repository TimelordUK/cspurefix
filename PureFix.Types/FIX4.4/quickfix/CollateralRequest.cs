using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AX", FixVersion.FIX44)]
	public sealed class CollateralRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 894, Type = TagType.String, Offset = 1)]
		public string? CollReqID { get; set; }
		
		[TagDetails(Tag = 895, Type = TagType.Int, Offset = 2)]
		public int? CollAsgnReason { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 3)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 4)]
		public DateTime? ExpireTime { get; set; }
		
		[Component(Offset = 5)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 7)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 8)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 9)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 10)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 11)]
		public string? SecondaryClOrdID { get; set; }
		
		[Component(Offset = 12)]
		public ExecCollGrp? ExecCollGrp { get; set; }
		
		[Component(Offset = 13)]
		public TrdCollGrp? TrdCollGrp { get; set; }
		
		[Component(Offset = 14)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 15)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 16)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 17)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 18)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 19)]
		public string? Currency { get; set; }
		
		[Component(Offset = 20)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 21)]
		public UndInstrmtCollGrp? UndInstrmtCollGrp { get; set; }
		
		[TagDetails(Tag = 899, Type = TagType.Float, Offset = 22)]
		public double? MarginExcess { get; set; }
		
		[TagDetails(Tag = 900, Type = TagType.Float, Offset = 23)]
		public double? TotalNetValue { get; set; }
		
		[TagDetails(Tag = 901, Type = TagType.Float, Offset = 24)]
		public double? CashOutstanding { get; set; }
		
		[Component(Offset = 25)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 26)]
		public string? Side { get; set; }
		
		[Component(Offset = 27)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 28)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 29)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 30)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 31)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 32)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 33)]
		public double? EndCash { get; set; }
		
		[Component(Offset = 34)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 35)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 36)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 37)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 38)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 39)]
		public string? SettlSessSubID { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 40)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 41)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 42)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 43)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 44)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
