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
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 559, Type = TagType.Int, Offset = 2, Required = true)]
		public int? SecurityListRequestType { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 9, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 10, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 11, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 12, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 13, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 15, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
