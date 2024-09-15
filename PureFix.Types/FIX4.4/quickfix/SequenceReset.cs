using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("4", FixVersion.FIX44)]
	public sealed partial class SequenceReset : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 123, Type = TagType.Boolean, Offset = 1, Required = false)]
		public bool? GapFillFlag { get; set; }
		
		[TagDetails(Tag = 36, Type = TagType.Int, Offset = 2, Required = true)]
		public int? NewSeqNo { get; set; }
		
		[Component(Offset = 3, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
