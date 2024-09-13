using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("C", FixVersion.FIX44)]
	public sealed class Email : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 164, Type = TagType.String, Offset = 1, Required = true)]
		public string? EmailThreadID { get; set; }
		
		[TagDetails(Tag = 94, Type = TagType.String, Offset = 2, Required = true)]
		public string? EmailType { get; set; }
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 3, Required = false)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(Tag = 147, Type = TagType.String, Offset = 4, Required = true)]
		public string? Subject { get; set; }
		
		[TagDetails(Tag = 356, Type = TagType.Length, Offset = 5, Required = false, LinksToTag = 357)]
		public int? EncodedSubjectLen { get; set; }
		
		[TagDetails(Tag = 357, Type = TagType.RawData, Offset = 6, Required = false, LinksToTag = 356)]
		public byte[]? EncodedSubject { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component(Offset = 8, Required = false)]
		public InstrmtGrp? InstrmtGrp { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 10, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 11, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 12, Required = false)]
		public string? ClOrdID { get; set; }
		
		[Component(Offset = 13, Required = true)]
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 14, Required = false, LinksToTag = 96)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 15, Required = false, LinksToTag = 95)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 16, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
