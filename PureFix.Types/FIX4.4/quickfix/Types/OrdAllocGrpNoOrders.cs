using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class OrdAllocGrpNoOrders : IFixValidator, IFixEncoder
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
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (NestedParties2 is not null) ((IFixEncoder)NestedParties2).Encode(writer);
			if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
			if (OrderAvgPx is not null) writer.WriteNumber(799, OrderAvgPx.Value);
			if (OrderBookingQty is not null) writer.WriteNumber(800, OrderBookingQty.Value);
		}
	}
}
