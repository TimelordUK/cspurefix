using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPositions
	{
		[TagDetails(703, TagType.String)]
		public string? PosType { get; set; }
		
		[TagDetails(704, TagType.Float)]
		public double? LongQty { get; set; }
		
		[TagDetails(705, TagType.Float)]
		public double? ShortQty { get; set; }
		
		[TagDetails(706, TagType.Int)]
		public int? PosQtyStatus { get; set; }
		
		[Component]
		public NestedParties? NestedParties { get; set; }
		
	}
}
