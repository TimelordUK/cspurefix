using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityList : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(320, TagType.String)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(322, TagType.String)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(560, TagType.Int)]
		public int? SecurityRequestResult { get; set; }
		
		[TagDetails(393, TagType.Int)]
		public int? TotNoRelatedSym { get; set; }
		
		[TagDetails(893, TagType.Boolean)]
		public bool? LastFragment { get; set; }
		
		[Component]
		public SecListGrp? SecListGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
