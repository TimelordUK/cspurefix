using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("L", FixVersion.FIX44)]
	public sealed class ListExecute : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2)]
		public string? ClientBidID { get; set; }
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 3)]
		public string? BidID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 4)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 8)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
