using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityStatus : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(324, TagType.String)]
		public string? SecurityStatusReqID { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(325, TagType.Boolean)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(326, TagType.Int)]
		public int? SecurityTradingStatus { get; set; }
		
		[TagDetails(291, TagType.String)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(292, TagType.String)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(327, TagType.String)]
		public string? HaltReasonChar { get; set; }
		
		[TagDetails(328, TagType.Boolean)]
		public bool? InViewOfCommon { get; set; }
		
		[TagDetails(329, TagType.Boolean)]
		public bool? DueToRelated { get; set; }
		
		[TagDetails(330, TagType.Float)]
		public double? BuyVolume { get; set; }
		
		[TagDetails(331, TagType.Float)]
		public double? SellVolume { get; set; }
		
		[TagDetails(332, TagType.Float)]
		public double? HighPx { get; set; }
		
		[TagDetails(333, TagType.Float)]
		public double? LowPx { get; set; }
		
		[TagDetails(31, TagType.Float)]
		public double? LastPx { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(334, TagType.Int)]
		public int? Adjustment { get; set; }
		
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
