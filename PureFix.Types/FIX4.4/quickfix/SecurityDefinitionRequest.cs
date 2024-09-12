using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("c", FixVersion.FIX44)]
	public sealed class SecurityDefinitionRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 321, Type = TagType.Int, Offset = 2)]
		public int? SecurityRequestType { get; set; }
		
		[Component(Offset = 3)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 4)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 5)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 10)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 11)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 12)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 13)]
		public int? ExpirationCycle { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 15)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
