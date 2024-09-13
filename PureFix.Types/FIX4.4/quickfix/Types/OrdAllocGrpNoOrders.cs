using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class OrdAllocGrpNoOrders
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 4, Required = false)]
		public string? ListID { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public NestedParties2? NestedParties2 { get; set; }
		
		[TagDetails(Tag = 38, Type = TagType.Float, Offset = 6, Required = false)]
		public double? OrderQty { get; set; }
		
		[TagDetails(Tag = 799, Type = TagType.Float, Offset = 7, Required = false)]
		public double? OrderAvgPx { get; set; }
		
		[TagDetails(Tag = 800, Type = TagType.Float, Offset = 8, Required = false)]
		public double? OrderBookingQty { get; set; }
		
	}
}
