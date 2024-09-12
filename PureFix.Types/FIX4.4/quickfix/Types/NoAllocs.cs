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
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 1)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 736, Type = TagType.String, Offset = 2)]
		public string? AllocSettlCurrency { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 3)]
		public string? IndividualAllocID { get; set; }
		
		[Component(Offset = 4)]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 5)]
		public double? AllocQty { get; set; }
		
	}
}
