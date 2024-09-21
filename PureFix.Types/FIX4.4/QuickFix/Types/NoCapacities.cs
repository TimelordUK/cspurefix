using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NoCapacities : IFixGroup
	{
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 0, Required = true)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 863, Type = TagType.Float, Offset = 2, Required = true)]
		public double? OrderCapacityQty {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				OrderCapacity is not null
				&& OrderCapacityQty is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (OrderCapacityQty is not null) writer.WriteNumber(863, OrderCapacityQty.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OrderCapacity = view.GetString(528);
			OrderRestrictions = view.GetString(529);
			OrderCapacityQty = view.GetDouble(863);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "OrderCapacity":
					value = OrderCapacity;
					break;
				case "OrderRestrictions":
					value = OrderRestrictions;
					break;
				case "OrderCapacityQty":
					value = OrderCapacityQty;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			OrderCapacity = null;
			OrderRestrictions = null;
			OrderCapacityQty = null;
		}
	}
}
