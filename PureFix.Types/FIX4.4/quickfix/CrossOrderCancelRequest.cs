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
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(548, TagType.String)]
		public string? CrossID { get; set; }
		
		[TagDetails(551, TagType.String)]
		public string? OrigCrossID { get; set; }
		
		[TagDetails(549, TagType.Int)]
		public int? CrossType { get; set; }
		
		[TagDetails(550, TagType.Int)]
		public int? CrossPrioritization { get; set; }
		
		[Component]
		public SideCrossOrdCxlGrp? SideCrossOrdCxlGrp { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
