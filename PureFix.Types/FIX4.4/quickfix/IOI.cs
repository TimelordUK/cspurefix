using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("6", FixVersion.FIX44)]
	public sealed class IOI : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 1)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 28, Type = TagType.String, Offset = 2)]
		public string? IOITransType { get; set; }
		
		[TagDetails(Tag = 26, Type = TagType.String, Offset = 3)]
		public string? IOIRefID { get; set; }
		
		[Component(Offset = 4)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 5)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 6)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 7)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 8)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 9)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 27, Type = TagType.String, Offset = 10)]
		public string? IOIQty { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 11)]
		public string? Currency { get; set; }
		
		[Component(Offset = 12)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 13)]
		public InstrmtLegIOIGrp? InstrmtLegIOIGrp { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 14)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 15)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 16)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 25, Type = TagType.String, Offset = 17)]
		public string? IOIQltyInd { get; set; }
		
		[TagDetails(Tag = 130, Type = TagType.Boolean, Offset = 18)]
		public bool? IOINaturalFlag { get; set; }
		
		[Component(Offset = 19)]
		public IOIQualGrp? IOIQualGrp { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 20)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 21)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 22)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 23)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 24)]
		public string? URLLink { get; set; }
		
		[Component(Offset = 25)]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component(Offset = 26)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 27)]
		public YieldData? YieldData { get; set; }
		
		[Component(Offset = 28)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
