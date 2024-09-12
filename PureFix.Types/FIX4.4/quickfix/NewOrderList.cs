using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class NewOrderList : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(66, TagType.String)]
		public string? ListID { get; set; }
		
		[TagDetails(390, TagType.String)]
		public string? BidID { get; set; }
		
		[TagDetails(391, TagType.String)]
		public string? ClientBidID { get; set; }
		
		[TagDetails(414, TagType.Int)]
		public int? ProgRptReqs { get; set; }
		
		[TagDetails(394, TagType.Int)]
		public int? BidType { get; set; }
		
		[TagDetails(415, TagType.Int)]
		public int? ProgPeriodInterval { get; set; }
		
		[TagDetails(480, TagType.String)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(481, TagType.String)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(513, TagType.String)]
		public string? RegistID { get; set; }
		
		[TagDetails(433, TagType.String)]
		public string? ListExecInstType { get; set; }
		
		[TagDetails(69, TagType.String)]
		public string? ListExecInst { get; set; }
		
		[TagDetails(352, TagType.Length)]
		public int? EncodedListExecInstLen { get; set; }
		
		[TagDetails(353, TagType.RawData)]
		public byte[]? EncodedListExecInst { get; set; }
		
		[TagDetails(765, TagType.Float)]
		public double? AllowableOneSidednessPct { get; set; }
		
		[TagDetails(766, TagType.Float)]
		public double? AllowableOneSidednessValue { get; set; }
		
		[TagDetails(767, TagType.String)]
		public string? AllowableOneSidednessCurr { get; set; }
		
		[TagDetails(68, TagType.Int)]
		public int? TotNoOrders { get; set; }
		
		[TagDetails(893, TagType.Boolean)]
		public bool? LastFragment { get; set; }
		
		[Component]
		public ListOrdGrp? ListOrdGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
