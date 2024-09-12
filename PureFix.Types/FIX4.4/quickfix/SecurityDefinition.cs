using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("d", FixVersion.FIX44)]
	public sealed class SecurityDefinition : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(Tag = 323, Type = TagType.Int, Offset = 3)]
		public int? SecurityResponseType { get; set; }
		
		[Component(Offset = 4)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 5)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 6)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 7)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 10)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 11)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 12)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 13)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 14)]
		public int? ExpirationCycle { get; set; }
		
		[TagDetails(Tag = 561, Type = TagType.Float, Offset = 15)]
		public double? RoundLot { get; set; }
		
		[TagDetails(Tag = 562, Type = TagType.Float, Offset = 16)]
		public double? MinTradeVol { get; set; }
		
		[Component(Offset = 17)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
