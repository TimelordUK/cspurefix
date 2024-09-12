using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("2", FixVersion.FIX44)]
	public sealed class ResendRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 7, Type = TagType.Int, Offset = 1)]
		public int? BeginSeqNo { get; set; }
		
		[TagDetails(Tag = 16, Type = TagType.Int, Offset = 2)]
		public int? EndSeqNo { get; set; }
		
		[Component(Offset = 3)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
