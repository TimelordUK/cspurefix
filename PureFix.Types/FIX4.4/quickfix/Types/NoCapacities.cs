using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoCapacities
	{
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 0)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 1)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 863, Type = TagType.Float, Offset = 2)]
		public double? OrderCapacityQty { get; set; }
		
	}
}
