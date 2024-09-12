using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AA", FixVersion.FIX44)]
	public sealed class DerivativeSecurityList : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(320, TagType.String)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(322, TagType.String)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(560, TagType.Int)]
		public int? SecurityRequestResult { get; set; }
		
		[Component]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(393, TagType.Int)]
		public int? TotNoRelatedSym { get; set; }
		
		[TagDetails(893, TagType.Boolean)]
		public bool? LastFragment { get; set; }
		
		[Component]
		public RelSymDerivSecGrp? RelSymDerivSecGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
