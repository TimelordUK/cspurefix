using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("2", FixVersion.FIX44)]
	public sealed partial class ResendRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 7, Type = TagType.Int, Offset = 1, Required = true)]
		public int? BeginSeqNo { get; set; }
		
		[TagDetails(Tag = 16, Type = TagType.Int, Offset = 2, Required = true)]
		public int? EndSeqNo { get; set; }
		
		[Component(Offset = 3, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
