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
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 1, Required = true)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 28, Type = TagType.String, Offset = 2, Required = true)]
		public string? IOITransType { get; set; }
		
		[TagDetails(Tag = 26, Type = TagType.String, Offset = 3, Required = false)]
		public string? IOIRefID { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 7, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 8, Required = false)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 27, Type = TagType.String, Offset = 10, Required = true)]
		public string? IOIQty { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 11, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 12, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegIOIGrp? InstrmtLegIOIGrp { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 14, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 15, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 16, Required = false)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 25, Type = TagType.String, Offset = 17, Required = false)]
		public string? IOIQltyInd { get; set; }
		
		[TagDetails(Tag = 130, Type = TagType.Boolean, Offset = 18, Required = false)]
		public bool? IOINaturalFlag { get; set; }
		
		[Component(Offset = 19, Required = false)]
		public IOIQualGrp? IOIQualGrp { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 20, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 21, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 22, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 23, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 24, Required = false)]
		public string? URLLink { get; set; }
		
		[Component(Offset = 25, Required = false)]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component(Offset = 26, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 27, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[Component(Offset = 28, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
