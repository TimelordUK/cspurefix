using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("N", FixVersion.FIX44)]
	public sealed partial class ListStatus : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 429, Type = TagType.Int, Offset = 2, Required = true)]
		public int? ListStatusType { get; set; }
		
		[TagDetails(Tag = 82, Type = TagType.Int, Offset = 3, Required = true)]
		public int? NoRpts { get; set; }
		
		[TagDetails(Tag = 431, Type = TagType.Int, Offset = 4, Required = true)]
		public int? ListOrderStatus { get; set; }
		
		[TagDetails(Tag = 83, Type = TagType.Int, Offset = 5, Required = true)]
		public int? RptSeq { get; set; }
		
		[TagDetails(Tag = 444, Type = TagType.String, Offset = 6, Required = false)]
		public string? ListStatusText { get; set; }
		
		[TagDetails(Tag = 445, Type = TagType.Length, Offset = 7, Required = false, LinksToTag = 446)]
		public int? EncodedListStatusTextLen { get; set; }
		
		[TagDetails(Tag = 446, Type = TagType.RawData, Offset = 8, Required = false, LinksToTag = 445)]
		public byte[]? EncodedListStatusText { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 68, Type = TagType.Int, Offset = 10, Required = true)]
		public int? TotNoOrders { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 11, Required = false)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public OrdListStatGrp? OrdListStatGrp { get; set; }
		
		[Component(Offset = 13, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
