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
		[TagDetails(528, TagType.String)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(529, TagType.String)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(863, TagType.Float)]
		public double? OrderCapacityQty { get; set; }
		
	}
}
