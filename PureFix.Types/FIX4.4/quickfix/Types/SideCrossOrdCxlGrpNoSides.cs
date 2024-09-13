using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class SideCrossOrdCxlGrpNoSides
	{
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 2, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 4, Required = false)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 586, Type = TagType.UtcTimestamp, Offset = 5, Required = false)]
		public DateTime? OrigOrdModTime { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 7, Required = false)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 8, Required = false)]
		public DateTime? TradeDate { get; set; }
		
		[Component(Offset = 9, Required = true)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 10, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 11, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
	}
}
