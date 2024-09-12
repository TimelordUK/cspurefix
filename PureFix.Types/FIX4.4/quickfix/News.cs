using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("B")]
	public sealed class News : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(42, TagType.UtcTimestamp)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(61, TagType.String)]
		public string? Urgency { get; set; }
		
		[TagDetails(148, TagType.String)]
		public string? Headline { get; set; }
		
		[TagDetails(358, TagType.Length)]
		public int? EncodedHeadlineLen { get; set; }
		
		[TagDetails(359, TagType.RawData)]
		public byte[]? EncodedHeadline { get; set; }
		
		[Component]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component]
		public InstrmtGrp? InstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		
		[TagDetails(149, TagType.String)]
		public string? URLLink { get; set; }
		
		[TagDetails(95, TagType.Length)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(96, TagType.RawData)]
		public byte[]? RawData { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
