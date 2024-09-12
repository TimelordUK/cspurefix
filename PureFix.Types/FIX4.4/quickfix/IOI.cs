using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class IOI : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(23, TagType.String)]
		public string? IOIID { get; set; }
		
		[TagDetails(28, TagType.String)]
		public string? IOITransType { get; set; }
		
		[TagDetails(26, TagType.String)]
		public string? IOIRefID { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(27, TagType.String)]
		public string? IOIQty { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[Component]
		public Stipulations? Stipulations { get; set; }
		
		[Component]
		public InstrmtLegIOIGrp? InstrmtLegIOIGrp { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[TagDetails(44, TagType.Float)]
		public double? Price { get; set; }
		
		[TagDetails(62, TagType.UtcTimestamp)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(25, TagType.String)]
		public string? IOIQltyInd { get; set; }
		
		[TagDetails(130, TagType.Boolean)]
		public bool? IOINaturalFlag { get; set; }
		
		[Component]
		public IOIQualGrp? IOIQualGrp { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(149, TagType.String)]
		public string? URLLink { get; set; }
		
		[Component]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component]
		public YieldData? YieldData { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
