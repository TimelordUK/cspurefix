using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("4", FixVersion.FIX44)]
	public sealed class SequenceReset : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 123, Type = TagType.Boolean, Offset = 1, Required = false)]
		public bool? GapFillFlag { get; set; }
		
		[TagDetails(Tag = 36, Type = TagType.Int, Offset = 2, Required = true)]
		public int? NewSeqNo { get; set; }
		
		[Component(Offset = 3, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
