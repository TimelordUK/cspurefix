using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("f", FixVersion.FIX44)]
	public sealed class SecurityStatus : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 324, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecurityStatusReqID { get; set; }
		
		[Component(Offset = 2, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 7, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 9, Required = false)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 326, Type = TagType.Int, Offset = 10, Required = false)]
		public int? SecurityTradingStatus { get; set; }
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 11, Required = false)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 12, Required = false)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(Tag = 327, Type = TagType.String, Offset = 13, Required = false)]
		public string? HaltReasonChar { get; set; }
		
		[TagDetails(Tag = 328, Type = TagType.Boolean, Offset = 14, Required = false)]
		public bool? InViewOfCommon { get; set; }
		
		[TagDetails(Tag = 329, Type = TagType.Boolean, Offset = 15, Required = false)]
		public bool? DueToRelated { get; set; }
		
		[TagDetails(Tag = 330, Type = TagType.Float, Offset = 16, Required = false)]
		public double? BuyVolume { get; set; }
		
		[TagDetails(Tag = 331, Type = TagType.Float, Offset = 17, Required = false)]
		public double? SellVolume { get; set; }
		
		[TagDetails(Tag = 332, Type = TagType.Float, Offset = 18, Required = false)]
		public double? HighPx { get; set; }
		
		[TagDetails(Tag = 333, Type = TagType.Float, Offset = 19, Required = false)]
		public double? LowPx { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 20, Required = false)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 21, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 334, Type = TagType.Int, Offset = 22, Required = false)]
		public int? Adjustment { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 23, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 24, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 25, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 26, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
