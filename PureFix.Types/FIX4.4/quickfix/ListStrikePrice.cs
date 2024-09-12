using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("m", FixVersion.FIX44)]
	public sealed class ListStrikePrice : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 422, Type = TagType.Int, Offset = 2)]
		public int? TotNoStrikes { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 3)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 4)]
		public InstrmtStrkPxGrp? InstrmtStrkPxGrp { get; set; }
		
		[Component(Offset = 5)]
		public UndInstrmtStrkPxGrp? UndInstrmtStrkPxGrp { get; set; }
		
		[Component(Offset = 6)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
