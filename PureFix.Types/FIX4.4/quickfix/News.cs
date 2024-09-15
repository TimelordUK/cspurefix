using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("B", FixVersion.FIX44)]
	public sealed partial class News : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 1, Required = false)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(Tag = 61, Type = TagType.String, Offset = 2, Required = false)]
		public string? Urgency { get; set; }
		
		[TagDetails(Tag = 148, Type = TagType.String, Offset = 3, Required = true)]
		public string? Headline { get; set; }
		
		[TagDetails(Tag = 358, Type = TagType.Length, Offset = 4, Required = false, LinksToTag = 359)]
		public int? EncodedHeadlineLen { get; set; }
		
		[TagDetails(Tag = 359, Type = TagType.RawData, Offset = 5, Required = false, LinksToTag = 358)]
		public byte[]? EncodedHeadline { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public InstrmtGrp? InstrmtGrp { get; set; }
		
		[Component(Offset = 8, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 10, Required = true)]
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 11, Required = false)]
		public string? URLLink { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 96)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 95)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 14, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
