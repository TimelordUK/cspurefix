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
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(164, TagType.String)]
		public string? EmailThreadID { get; set; }
		
		[TagDetails(94, TagType.String)]
		public string? EmailType { get; set; }
		
		[TagDetails(42, TagType.UtcTimestamp)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(147, TagType.String)]
		public string? Subject { get; set; }
		
		[TagDetails(356, TagType.Length)]
		public int? EncodedSubjectLen { get; set; }
		
		[TagDetails(357, TagType.RawData)]
		public byte[]? EncodedSubject { get; set; }
		
		[Component]
		public RoutingGrp? RoutingGrp { get; set; }
		
		[Component]
		public InstrmtGrp? InstrmtGrp { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[Component]
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		
		[TagDetails(95, TagType.Length)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(96, TagType.RawData)]
		public byte[]? RawData { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
