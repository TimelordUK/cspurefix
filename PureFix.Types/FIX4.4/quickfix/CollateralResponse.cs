using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AZ", FixVersion.FIX44)]
	public sealed class CollateralResponse : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 904, Type = TagType.String, Offset = 1)]
		public string? CollRespID { get; set; }
		
		[TagDetails(Tag = 902, Type = TagType.String, Offset = 2)]
		public string? CollAsgnID { get; set; }
		
		[TagDetails(Tag = 894, Type = TagType.String, Offset = 3)]
		public string? CollReqID { get; set; }
		
		[TagDetails(Tag = 895, Type = TagType.Int, Offset = 4)]
		public int? CollAsgnReason { get; set; }
		
		[TagDetails(Tag = 903, Type = TagType.Int, Offset = 5)]
		public int? CollAsgnTransType { get; set; }
		
		[TagDetails(Tag = 905, Type = TagType.Int, Offset = 6)]
		public int? CollAsgnRespType { get; set; }
		
		[TagDetails(Tag = 906, Type = TagType.Int, Offset = 7)]
		public int? CollAsgnRejectReason { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 8)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 9)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 10)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 11)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 12)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 13)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 14)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 15)]
		public string? SecondaryClOrdID { get; set; }
		
		[Component(Offset = 16)]
		public ExecCollGrp? ExecCollGrp { get; set; }
		
		[Component(Offset = 17)]
		public TrdCollGrp? TrdCollGrp { get; set; }
		
		[Component(Offset = 18)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 19)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 20)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 21)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 22)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 23)]
		public string? Currency { get; set; }
		
		[Component(Offset = 24)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 25)]
		public UndInstrmtCollGrp? UndInstrmtCollGrp { get; set; }
		
		[TagDetails(Tag = 899, Type = TagType.Float, Offset = 26)]
		public double? MarginExcess { get; set; }
		
		[TagDetails(Tag = 900, Type = TagType.Float, Offset = 27)]
		public double? TotalNetValue { get; set; }
		
		[TagDetails(Tag = 901, Type = TagType.Float, Offset = 28)]
		public double? CashOutstanding { get; set; }
		
		[Component(Offset = 29)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30)]
		public string? Side { get; set; }
		
		[Component(Offset = 31)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 32)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 33)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 34)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 35)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 36)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 37)]
		public double? EndCash { get; set; }
		
		[Component(Offset = 38)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 39)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 40)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 41)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 42)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 43)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
