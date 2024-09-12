using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("f", FixVersion.FIX44)]
	public sealed class SecurityStatus : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 324, Type = TagType.String, Offset = 1)]
		public string? SecurityStatusReqID { get; set; }
		
		[Component(Offset = 2)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 3)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 4)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 5)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 7)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 8)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 9)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 326, Type = TagType.Int, Offset = 10)]
		public int? SecurityTradingStatus { get; set; }
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 11)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 12)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(Tag = 327, Type = TagType.String, Offset = 13)]
		public string? HaltReasonChar { get; set; }
		
		[TagDetails(Tag = 328, Type = TagType.Boolean, Offset = 14)]
		public bool? InViewOfCommon { get; set; }
		
		[TagDetails(Tag = 329, Type = TagType.Boolean, Offset = 15)]
		public bool? DueToRelated { get; set; }
		
		[TagDetails(Tag = 330, Type = TagType.Float, Offset = 16)]
		public double? BuyVolume { get; set; }
		
		[TagDetails(Tag = 331, Type = TagType.Float, Offset = 17)]
		public double? SellVolume { get; set; }
		
		[TagDetails(Tag = 332, Type = TagType.Float, Offset = 18)]
		public double? HighPx { get; set; }
		
		[TagDetails(Tag = 333, Type = TagType.Float, Offset = 19)]
		public double? LowPx { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 20)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 21)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 334, Type = TagType.Int, Offset = 22)]
		public int? Adjustment { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 23)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 24)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 25)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 26)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
