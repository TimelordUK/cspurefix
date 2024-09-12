using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("7", FixVersion.FIX44)]
	public sealed class Advertisement : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 2, Type = TagType.String, Offset = 1)]
		public string? AdvId { get; set; }
		
		[TagDetails(Tag = 5, Type = TagType.String, Offset = 2)]
		public string? AdvTransType { get; set; }
		
		[TagDetails(Tag = 3, Type = TagType.String, Offset = 3)]
		public string? AdvRefID { get; set; }
		
		[Component(Offset = 4)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 5)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 6)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 4, Type = TagType.String, Offset = 7)]
		public string? AdvSide { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 8)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 9)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 10)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 11)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 12)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 13)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 14)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 15)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 16)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 17)]
		public string? URLLink { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 18)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 19)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 20)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 21)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
