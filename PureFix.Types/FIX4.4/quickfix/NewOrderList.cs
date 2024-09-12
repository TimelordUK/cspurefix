using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("E", FixVersion.FIX44)]
	public sealed class NewOrderList : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 2)]
		public string? BidID { get; set; }
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 3)]
		public string? ClientBidID { get; set; }
		
		[TagDetails(Tag = 414, Type = TagType.Int, Offset = 4)]
		public int? ProgRptReqs { get; set; }
		
		[TagDetails(Tag = 394, Type = TagType.Int, Offset = 5)]
		public int? BidType { get; set; }
		
		[TagDetails(Tag = 415, Type = TagType.Int, Offset = 6)]
		public int? ProgPeriodInterval { get; set; }
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 7)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 8)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 9)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 433, Type = TagType.String, Offset = 10)]
		public string? ListExecInstType { get; set; }
		
		[TagDetails(Tag = 69, Type = TagType.String, Offset = 11)]
		public string? ListExecInst { get; set; }
		
		[TagDetails(Tag = 352, Type = TagType.Length, Offset = 12)]
		public int? EncodedListExecInstLen { get; set; }
		
		[TagDetails(Tag = 353, Type = TagType.RawData, Offset = 13)]
		public byte[]? EncodedListExecInst { get; set; }
		
		[TagDetails(Tag = 765, Type = TagType.Float, Offset = 14)]
		public double? AllowableOneSidednessPct { get; set; }
		
		[TagDetails(Tag = 766, Type = TagType.Float, Offset = 15)]
		public double? AllowableOneSidednessValue { get; set; }
		
		[TagDetails(Tag = 767, Type = TagType.String, Offset = 16)]
		public string? AllowableOneSidednessCurr { get; set; }
		
		[TagDetails(Tag = 68, Type = TagType.Int, Offset = 17)]
		public int? TotNoOrders { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 18)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 19)]
		public ListOrdGrp? ListOrdGrp { get; set; }
		
		[Component(Offset = 20)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
