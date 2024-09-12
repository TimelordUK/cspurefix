using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("y", FixVersion.FIX44)]
	public sealed class SecurityList : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(Tag = 560, Type = TagType.Int, Offset = 3)]
		public int? SecurityRequestResult { get; set; }
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 4)]
		public int? TotNoRelatedSym { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 5)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 6)]
		public SecListGrp? SecListGrp { get; set; }
		
		[Component(Offset = 7)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
