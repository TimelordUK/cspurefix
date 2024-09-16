using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPricingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41607, Offset = 0, Required = false)]
		public NoLegPricingDateBusinessCenters[]? NoLegPricingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPricingDateBusinessCenters is not null && NoLegPricingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41607, NoLegPricingDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPricingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPricingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPricingDateBusinessCenters") is IMessageView viewNoLegPricingDateBusinessCenters)
			{
				var count = viewNoLegPricingDateBusinessCenters.GroupCount();
				NoLegPricingDateBusinessCenters = new NoLegPricingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPricingDateBusinessCenters[i] = new();
					((IFixParser)NoLegPricingDateBusinessCenters[i]).Parse(viewNoLegPricingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPricingDateBusinessCenters":
					value = NoLegPricingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
