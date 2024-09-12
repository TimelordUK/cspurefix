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
		[TagDetails(Tag = 703, Type = TagType.String, Offset = 0, Required = false)]
		public string? PosType { get; set; }
		
		[TagDetails(Tag = 704, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LongQty { get; set; }
		
		[TagDetails(Tag = 705, Type = TagType.Float, Offset = 2, Required = false)]
		public double? ShortQty { get; set; }
		
		[TagDetails(Tag = 706, Type = TagType.Int, Offset = 3, Required = false)]
		public int? PosQtyStatus { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
	}
}
