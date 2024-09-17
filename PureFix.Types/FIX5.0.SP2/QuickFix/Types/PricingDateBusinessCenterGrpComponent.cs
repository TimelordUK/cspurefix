using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PricingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41230, Offset = 0, Required = false)]
		public NoPricingDateBusinessCenters[]? NoPricingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPricingDateBusinessCenters is not null && NoPricingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41230, NoPricingDateBusinessCenters.Length);
				for (int i = 0; i < NoPricingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPricingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPricingDateBusinessCenters") is IMessageView viewNoPricingDateBusinessCenters)
			{
				var count = viewNoPricingDateBusinessCenters.GroupCount();
				NoPricingDateBusinessCenters = new NoPricingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPricingDateBusinessCenters[i] = new();
					((IFixParser)NoPricingDateBusinessCenters[i]).Parse(viewNoPricingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPricingDateBusinessCenters":
					value = NoPricingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
