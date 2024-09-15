using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PreAllocMlegGrpNoAllocs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 1, Required = false)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 736, Type = TagType.String, Offset = 2, Required = false)]
		public string? AllocSettlCurrency { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 3, Required = false)]
		public string? IndividualAllocID { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public NestedParties3? NestedParties3 { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 5, Required = false)]
		public double? AllocQty { get; set; }
		
	}
}
