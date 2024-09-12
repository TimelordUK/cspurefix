using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("F", FixVersion.FIX44)]
	public sealed class OrderCancelRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 1)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 2)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 4)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 5)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 6)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 586, Type = TagType.UtcTimestamp, Offset = 7)]
		public DateTime? OrigOrdModTime { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 11)]
		public Parties? Parties { get; set; }
		
		[Component(Offset = 12)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 13)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 14)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 15)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 16)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 17)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 18)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 19)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 20)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 21)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 22)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
