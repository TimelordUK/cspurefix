using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AO", FixVersion.FIX44)]
	public sealed class RequestForPositionsAck : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosMaintRptID { get; set; }
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 2, Required = false)]
		public string? PosReqID { get; set; }
		
		[TagDetails(Tag = 727, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TotalNumPosReports { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 728, Type = TagType.Int, Offset = 5, Required = true)]
		public int? PosReqResult { get; set; }
		
		[TagDetails(Tag = 729, Type = TagType.Int, Offset = 6, Required = true)]
		public int? PosReqStatus { get; set; }
		
		[Component(Offset = 7, Required = true)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8, Required = true)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10, Required = true)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 12, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 14, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 15, Required = false)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 16, Required = false)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 17, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 18, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 19, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 20, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
