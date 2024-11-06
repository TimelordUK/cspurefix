using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class AllocationNoOrders : IFixGroup
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 3, Required = false)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 105, Type = TagType.String, Offset = 4, Required = false)]
		public string? WaveNo {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (WaveNo is not null) writer.WriteString(105, WaveNo);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ClOrdID = view.GetString(11);
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			ListID = view.GetString(66);
			WaveNo = view.GetString(105);
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
				case "ListID":
					value = ListID;
					break;
				case "WaveNo":
					value = WaveNo;
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
			ListID = null;
			WaveNo = null;
		}
	}
}
