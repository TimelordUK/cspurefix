using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("H", FixVersion.FIX44)]
	public sealed class OrderStatusRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 2)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 4)]
		public string? ClOrdLinkID { get; set; }
		
		[Component(Offset = 5)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 790, Type = TagType.String, Offset = 6)]
		public string? OrdStatusReqID { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 8)]
		public int? AcctIDSource { get; set; }
		
		[Component(Offset = 9)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 10)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 11)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 12)]
		public string? Side { get; set; }
		
		[Component(Offset = 13)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
