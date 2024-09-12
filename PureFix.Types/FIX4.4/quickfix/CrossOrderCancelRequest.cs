using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("u", FixVersion.FIX44)]
	public sealed class CrossOrderCancelRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 2)]
		public string? CrossID { get; set; }
		
		[TagDetails(Tag = 551, Type = TagType.String, Offset = 3)]
		public string? OrigCrossID { get; set; }
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 4)]
		public int? CrossType { get; set; }
		
		[TagDetails(Tag = 550, Type = TagType.Int, Offset = 5)]
		public int? CrossPrioritization { get; set; }
		
		[Component(Offset = 6)]
		public SideCrossOrdCxlGrp? SideCrossOrdCxlGrp { get; set; }
		
		[Component(Offset = 7)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 8)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 9)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 10)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 11)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
