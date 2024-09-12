using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("9", FixVersion.FIX44)]
	public sealed class OrderCancelReject : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 4, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 5, Required = false)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 6, Required = true)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 7, Required = true)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? WorkingIndicator { get; set; }
		
		[TagDetails(Tag = 586, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? OrigOrdModTime { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 10, Required = false)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 11, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 12, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 13, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 14, Required = false)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 15, Required = false)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 16, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 434, Type = TagType.String, Offset = 17, Required = true)]
		public string? CxlRejResponseTo { get; set; }
		
		[TagDetails(Tag = 102, Type = TagType.Int, Offset = 18, Required = false)]
		public int? CxlRejReason { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 19, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 20, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 21, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 22, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
