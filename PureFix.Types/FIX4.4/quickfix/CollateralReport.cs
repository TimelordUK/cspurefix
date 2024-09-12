using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BA", FixVersion.FIX44)]
	public sealed class CollateralReport : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 908, Type = TagType.String, Offset = 1)]
		public string? CollRptID { get; set; }
		
		[TagDetails(Tag = 909, Type = TagType.String, Offset = 2)]
		public string? CollInquiryID { get; set; }
		
		[TagDetails(Tag = 910, Type = TagType.Int, Offset = 3)]
		public int? CollStatus { get; set; }
		
		[TagDetails(Tag = 911, Type = TagType.Int, Offset = 4)]
		public int? TotNumReports { get; set; }
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 5)]
		public bool? LastRptRequested { get; set; }
		
		[Component(Offset = 6)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 8)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 9)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 10)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 11)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 12)]
		public string? SecondaryClOrdID { get; set; }
		
		[Component(Offset = 13)]
		public ExecCollGrp? ExecCollGrp { get; set; }
		
		[Component(Offset = 14)]
		public TrdCollGrp? TrdCollGrp { get; set; }
		
		[Component(Offset = 15)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 16)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 17)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 18)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 19)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 20)]
		public string? Currency { get; set; }
		
		[Component(Offset = 21)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 22)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 899, Type = TagType.Float, Offset = 23)]
		public double? MarginExcess { get; set; }
		
		[TagDetails(Tag = 900, Type = TagType.Float, Offset = 24)]
		public double? TotalNetValue { get; set; }
		
		[TagDetails(Tag = 901, Type = TagType.Float, Offset = 25)]
		public double? CashOutstanding { get; set; }
		
		[Component(Offset = 26)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 27)]
		public string? Side { get; set; }
		
		[Component(Offset = 28)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 29)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 30)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 31)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 32)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 33)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 34)]
		public double? EndCash { get; set; }
		
		[Component(Offset = 35)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 36)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 37)]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 38)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 39)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 40)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 41)]
		public string? SettlSessSubID { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 42)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 43)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 44)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 45)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 46)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
