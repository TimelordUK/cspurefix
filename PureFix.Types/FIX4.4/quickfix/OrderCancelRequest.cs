using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class OrderCancelRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(41, TagType.String)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(526, TagType.String)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(583, TagType.String)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(66, TagType.String)]
		public string? ListID { get; set; }
		
		[TagDetails(586, TagType.UtcTimestamp)]
		public DateTime? OrigOrdModTime { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(376, TagType.String)]
		public string? ComplianceID { get; set; }
		
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
