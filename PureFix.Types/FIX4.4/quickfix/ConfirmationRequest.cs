using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BH", FixVersion.FIX44)]
	public sealed class ConfirmationRequest : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 859, Type = TagType.String, Offset = 1, Required = true)]
		public string? ConfirmReqID { get; set; }
		
		[TagDetails(Tag = 773, Type = TagType.Int, Offset = 2, Required = true)]
		public int? ConfirmType { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 4, Required = false)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 5, Required = false)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 6, Required = false)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 7, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 8, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 798, Type = TagType.Int, Offset = 10, Required = false)]
		public int? AllocAccountType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 11, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 14, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
