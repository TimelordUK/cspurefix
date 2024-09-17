using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NotAffectedOrdGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1370, Offset = 0, Required = false)]
		public NoNotAffectedOrders[]? NoNotAffectedOrders {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNotAffectedOrders is not null && NoNotAffectedOrders.Length != 0)
			{
				writer.WriteWholeNumber(1370, NoNotAffectedOrders.Length);
				for (int i = 0; i < NoNotAffectedOrders.Length; i++)
				{
					((IFixEncoder)NoNotAffectedOrders[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNotAffectedOrders") is IMessageView viewNoNotAffectedOrders)
			{
				var count = viewNoNotAffectedOrders.GroupCount();
				NoNotAffectedOrders = new NoNotAffectedOrders[count];
				for (int i = 0; i < count; i++)
				{
					NoNotAffectedOrders[i] = new();
					((IFixParser)NoNotAffectedOrders[i]).Parse(viewNoNotAffectedOrders.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNotAffectedOrders":
					value = NoNotAffectedOrders;
					break;
				default: return false;
			}
			return true;
		}
	}
}
