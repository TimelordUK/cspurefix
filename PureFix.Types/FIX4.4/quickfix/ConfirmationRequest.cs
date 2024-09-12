using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ConfirmationRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(859, TagType.String)]
		public string? ConfirmReqID { get; set; }
		
		[TagDetails(773, TagType.Int)]
		public int? ConfirmType { get; set; }
		
		[Component]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[TagDetails(70, TagType.String)]
		public string? AllocID { get; set; }
		
		[TagDetails(793, TagType.String)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(467, TagType.String)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(79, TagType.String)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(661, TagType.Int)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(798, TagType.Int)]
		public int? AllocAccountType { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
