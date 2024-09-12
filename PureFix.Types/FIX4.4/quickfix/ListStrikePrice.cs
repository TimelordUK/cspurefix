using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("m")]
	public sealed class ListStrikePrice : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(66, TagType.String)]
		public string? ListID { get; set; }
		
		[TagDetails(422, TagType.Int)]
		public int? TotNoStrikes { get; set; }
		
		[TagDetails(893, TagType.Boolean)]
		public bool? LastFragment { get; set; }
		
		[Component]
		public InstrmtStrkPxGrp? InstrmtStrkPxGrp { get; set; }
		
		[Component]
		public UndInstrmtStrkPxGrp? UndInstrmtStrkPxGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
