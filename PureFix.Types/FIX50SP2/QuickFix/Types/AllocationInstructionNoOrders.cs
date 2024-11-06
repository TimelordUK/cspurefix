using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AllocationInstructionNoOrders : IFixGroup
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 4, Required = false)]
		public string? ListID {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public NestedParties2Component? NestedParties2 {get; set;}
		
		[TagDetails(Tag = 38, Type = TagType.Float, Offset = 6, Required = false)]
		public double? OrderQty {get; set;}
		
		[TagDetails(Tag = 799, Type = TagType.Float, Offset = 7, Required = false)]
		public double? OrderAvgPx {get; set;}
		
		[TagDetails(Tag = 800, Type = TagType.Float, Offset = 8, Required = false)]
		public double? OrderBookingQty {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 9, Required = false)]
		public string? OrdType {get; set;}
		
		
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
			if (OrdType is not null) writer.WriteString(40, OrdType);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ClOrdID = view.GetString(11);
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			SecondaryClOrdID = view.GetString(526);
			ListID = view.GetString(66);
			if (view.GetView("NestedParties2") is IMessageView viewNestedParties2)
			{
				NestedParties2 = new();
				((IFixParser)NestedParties2).Parse(viewNestedParties2);
			}
			OrderQty = view.GetDouble(38);
			OrderAvgPx = view.GetDouble(799);
			OrderBookingQty = view.GetDouble(800);
			OrdType = view.GetString(40);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "OrderID":
					value = OrderID;
					break;
				case "SecondaryOrderID":
					value = SecondaryOrderID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "ListID":
					value = ListID;
					break;
				case "NestedParties2":
					value = NestedParties2;
					break;
				case "OrderQty":
					value = OrderQty;
					break;
				case "OrderAvgPx":
					value = OrderAvgPx;
					break;
				case "OrderBookingQty":
					value = OrderBookingQty;
					break;
				case "OrdType":
					value = OrdType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ClOrdID = null;
			OrderID = null;
			SecondaryOrderID = null;
			SecondaryClOrdID = null;
			ListID = null;
			((IFixReset?)NestedParties2)?.Reset();
			OrderQty = null;
			OrderAvgPx = null;
			OrderBookingQty = null;
			OrdType = null;
		}
	}
}
