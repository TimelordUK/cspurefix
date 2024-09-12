using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityDefinition : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(320, TagType.String)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(322, TagType.String)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(323, TagType.Int)]
		public int? SecurityResponseType { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(827, TagType.Int)]
		public int? ExpirationCycle { get; set; }
		
		[TagDetails(561, TagType.Float)]
		public double? RoundLot { get; set; }
		
		[TagDetails(562, TagType.Float)]
		public double? MinTradeVol { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
