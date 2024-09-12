using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class OrderMassCancelReport : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(526, TagType.String)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(198, TagType.String)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(530, TagType.String)]
		public string? MassCancelRequestType { get; set; }
		
		[TagDetails(531, TagType.String)]
		public string? MassCancelResponse { get; set; }
		
		[TagDetails(532, TagType.String)]
		public string? MassCancelRejectReason { get; set; }
		
		[TagDetails(533, TagType.Int)]
		public int? TotalAffectedOrders { get; set; }
		
		[Component]
		public AffectedOrdGrp? AffectedOrdGrp { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
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
