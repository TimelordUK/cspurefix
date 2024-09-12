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
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2, Required = true)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(Tag = 323, Type = TagType.Int, Offset = 3, Required = true)]
		public int? SecurityResponseType { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 7, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 10, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 11, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 12, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 14, Required = false)]
		public int? ExpirationCycle { get; set; }
		
		[TagDetails(Tag = 561, Type = TagType.Float, Offset = 15, Required = false)]
		public double? RoundLot { get; set; }
		
		[TagDetails(Tag = 562, Type = TagType.Float, Offset = 16, Required = false)]
		public double? MinTradeVol { get; set; }
		
		[Component(Offset = 17, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
