using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPricingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41947, Offset = 0, Required = false)]
		public IOINoUnderlyingPricingDateBusinessCenters[]? NoUnderlyingPricingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPricingDateBusinessCenters is not null && NoUnderlyingPricingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41947, NoUnderlyingPricingDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPricingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPricingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPricingDateBusinessCenters") is IMessageView viewNoUnderlyingPricingDateBusinessCenters)
			{
				var count = viewNoUnderlyingPricingDateBusinessCenters.GroupCount();
				NoUnderlyingPricingDateBusinessCenters = new IOINoUnderlyingPricingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPricingDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPricingDateBusinessCenters[i]).Parse(viewNoUnderlyingPricingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPricingDateBusinessCenters":
					value = NoUnderlyingPricingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPricingDateBusinessCenters = null;
		}
	}
}
