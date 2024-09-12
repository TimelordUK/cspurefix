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
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 164, Type = TagType.String, Offset = 1)]
		public string? EmailThreadID { get; set; }
		
		[TagDetails(Tag = 94, Type = TagType.String, Offset = 2)]
		public string? EmailType { get; set; }
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 3)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(Tag = 147, Type = TagType.String, Offset = 4)]
		public string? Subject { get; set; }
		
		[TagDetails(Tag = 356, Type = TagType.Length, Offset = 5)]
		public int? EncodedSubjectLen { get; set; }
		
		[TagDetails(Tag = 357, Type = TagType.RawData, Offset = 6)]
		public byte[]? EncodedSubject { get; set; }
		
		[Component(Offset = 7)]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component(Offset = 8)]
		public InstrmtGrp? InstrmtGrp { get; set; }
		
		[Component(Offset = 9)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 10)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 11)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 12)]
		public string? ClOrdID { get; set; }
		
		[Component(Offset = 13)]
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 14)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 15)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 16)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
