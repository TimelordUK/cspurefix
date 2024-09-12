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
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(123, TagType.Boolean)]
		public bool? GapFillFlag { get; set; }
		
		[TagDetails(36, TagType.Int)]
		public int? NewSeqNo { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
