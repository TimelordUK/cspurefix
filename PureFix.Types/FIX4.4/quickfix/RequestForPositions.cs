using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AN", FixVersion.FIX44)]
	public sealed class RequestForPositions : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 1)]
		public string? PosReqID { get; set; }
		
		[TagDetails(Tag = 724, Type = TagType.Int, Offset = 2)]
		public int? PosReqType { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 3)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 4)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 5)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 7)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 8)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 9)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 10)]
		public string? Currency { get; set; }
		
		[Component(Offset = 11)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 12)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 13)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 14)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 15)]
		public string? SettlSessSubID { get; set; }
		
		[Component(Offset = 16)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 17)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 18)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 19)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 20)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 21)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 22)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 23)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
