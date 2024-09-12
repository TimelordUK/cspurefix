using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AF", FixVersion.FIX44)]
	public sealed class OrderMassStatusRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 584, Type = TagType.String, Offset = 1)]
		public string? MassStatusReqID { get; set; }
		
		[TagDetails(Tag = 585, Type = TagType.Int, Offset = 2)]
		public int? MassStatusReqType { get; set; }
		
		[Component(Offset = 3)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 4)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 5)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 6)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 7)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 8)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 9)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 10)]
		public string? Side { get; set; }
		
		[Component(Offset = 11)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
