using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("x", FixVersion.FIX44)]
	public sealed class SecurityListRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 559, Type = TagType.Int, Offset = 2)]
		public int? SecurityListRequestType { get; set; }
		
		[Component(Offset = 3)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 4)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 5)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 6)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 7)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 9)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 10)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 11)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 12)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 13)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 15)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
