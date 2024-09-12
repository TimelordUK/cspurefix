using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoAllocs
	{
		[TagDetails(79, TagType.String)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(661, TagType.Int)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(736, TagType.String)]
		public string? AllocSettlCurrency { get; set; }
		
		[TagDetails(467, TagType.String)]
		public string? IndividualAllocID { get; set; }
		
		[Component]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(80, TagType.Float)]
		public double? AllocQty { get; set; }
		
	}
}
