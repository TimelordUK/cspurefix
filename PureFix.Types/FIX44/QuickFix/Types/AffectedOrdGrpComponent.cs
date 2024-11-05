using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class AffectedOrdGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 534, Offset = 0, Required = false)]
		public OrderMassCancelReportAffectedOrdGrpNoAffectedOrders[]? NoAffectedOrders {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAffectedOrders is not null && NoAffectedOrders.Length != 0)
			{
				writer.WriteWholeNumber(534, NoAffectedOrders.Length);
				for (int i = 0; i < NoAffectedOrders.Length; i++)
				{
					((IFixEncoder)NoAffectedOrders[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAffectedOrders") is IMessageView viewNoAffectedOrders)
			{
				var count = viewNoAffectedOrders.GroupCount();
				NoAffectedOrders = new OrderMassCancelReportAffectedOrdGrpNoAffectedOrders[count];
				for (int i = 0; i < count; i++)
				{
					NoAffectedOrders[i] = new();
					((IFixParser)NoAffectedOrders[i]).Parse(viewNoAffectedOrders.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAffectedOrders":
					value = NoAffectedOrders;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAffectedOrders = null;
		}
	}
}
