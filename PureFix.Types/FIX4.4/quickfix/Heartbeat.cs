using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("0", FixVersion.FIX44)]
	public sealed class Heartbeat : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 112, Type = TagType.String, Offset = 1)]
		public string? TestReqID { get; set; }
		
		[Component(Offset = 2)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
