using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("7", FixVersion.FIX44)]
	public sealed class Advertisement : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 2, Type = TagType.String, Offset = 1, Required = true)]
		public string? AdvId { get; set; }
		
		[TagDetails(Tag = 5, Type = TagType.String, Offset = 2, Required = true)]
		public string? AdvTransType { get; set; }
		
		[TagDetails(Tag = 3, Type = TagType.String, Offset = 3, Required = false)]
		public string? AdvRefID { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 4, Type = TagType.String, Offset = 7, Required = true)]
		public string? AdvSide { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 8, Required = true)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 9, Required = false)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 10, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 11, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 12, Required = false)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 13, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 14, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 17, Required = false)]
		public string? URLLink { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 18, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 19, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 20, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 21, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
