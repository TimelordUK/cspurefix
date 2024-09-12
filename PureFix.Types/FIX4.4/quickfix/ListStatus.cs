using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ListStatus : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(66, TagType.String)]
		public string? ListID { get; set; }
		
		[TagDetails(429, TagType.Int)]
		public int? ListStatusType { get; set; }
		
		[TagDetails(82, TagType.Int)]
		public int? NoRpts { get; set; }
		
		[TagDetails(431, TagType.Int)]
		public int? ListOrderStatus { get; set; }
		
		[TagDetails(83, TagType.Int)]
		public int? RptSeq { get; set; }
		
		[TagDetails(444, TagType.String)]
		public string? ListStatusText { get; set; }
		
		[TagDetails(445, TagType.Length)]
		public int? EncodedListStatusTextLen { get; set; }
		
		[TagDetails(446, TagType.RawData)]
		public byte[]? EncodedListStatusText { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(68, TagType.Int)]
		public int? TotNoOrders { get; set; }
		
		[TagDetails(893, TagType.Boolean)]
		public bool? LastFragment { get; set; }
		
		[Component]
		public OrdListStatGrp? OrdListStatGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
