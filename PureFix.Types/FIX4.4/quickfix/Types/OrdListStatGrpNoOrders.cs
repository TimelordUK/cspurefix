using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class OrdListStatGrpNoOrders
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 2, Required = true)]
		public double? CumQty { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 3, Required = true)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? WorkingIndicator { get; set; }
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 5, Required = true)]
		public double? LeavesQty { get; set; }
		
		[TagDetails(Tag = 84, Type = TagType.Float, Offset = 6, Required = true)]
		public double? CxlQty { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 7, Required = true)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 8, Required = false)]
		public int? OrdRejReason { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 9, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 10, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 11, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
	}
}
