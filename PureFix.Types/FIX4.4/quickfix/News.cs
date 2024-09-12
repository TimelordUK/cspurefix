using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("B", FixVersion.FIX44)]
	public sealed class News : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 1)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(Tag = 61, Type = TagType.String, Offset = 2)]
		public string? Urgency { get; set; }
		
		[TagDetails(Tag = 148, Type = TagType.String, Offset = 3)]
		public string? Headline { get; set; }
		
		[TagDetails(Tag = 358, Type = TagType.Length, Offset = 4)]
		public int? EncodedHeadlineLen { get; set; }
		
		[TagDetails(Tag = 359, Type = TagType.RawData, Offset = 5)]
		public byte[]? EncodedHeadline { get; set; }
		
		[Component(Offset = 6)]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component(Offset = 7)]
		public InstrmtGrp? InstrmtGrp { get; set; }
		
		[Component(Offset = 8)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 9)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 10)]
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 11)]
		public string? URLLink { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 12)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 13)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 14)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
