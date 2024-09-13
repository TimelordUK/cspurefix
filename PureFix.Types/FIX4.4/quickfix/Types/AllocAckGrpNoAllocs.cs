using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class AllocAckGrpNoAllocs
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 1, Required = false)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 366, Type = TagType.Float, Offset = 2, Required = false)]
		public double? AllocPrice { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 3, Required = false)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(Tag = 776, Type = TagType.Int, Offset = 4, Required = false)]
		public int? IndividualAllocRejCode { get; set; }
		
		[TagDetails(Tag = 161, Type = TagType.String, Offset = 5, Required = false)]
		public string? AllocText { get; set; }
		
		[TagDetails(Tag = 360, Type = TagType.Length, Offset = 6, Required = false, LinksToTag = 361)]
		public int? EncodedAllocTextLen { get; set; }
		
		[TagDetails(Tag = 361, Type = TagType.RawData, Offset = 7, Required = false, LinksToTag = 360)]
		public byte[]? EncodedAllocText { get; set; }
		
	}
}
