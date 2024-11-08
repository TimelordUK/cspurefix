using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingRateSpreadStepGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43005, Offset = 0, Required = false)]
		public IOINoUnderlyingRateSpreadSteps[]? NoUnderlyingRateSpreadSteps {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingRateSpreadSteps is not null && NoUnderlyingRateSpreadSteps.Length != 0)
			{
				writer.WriteWholeNumber(43005, NoUnderlyingRateSpreadSteps.Length);
				for (int i = 0; i < NoUnderlyingRateSpreadSteps.Length; i++)
				{
					((IFixEncoder)NoUnderlyingRateSpreadSteps[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingRateSpreadSteps") is IMessageView viewNoUnderlyingRateSpreadSteps)
			{
				var count = viewNoUnderlyingRateSpreadSteps.GroupCount();
				NoUnderlyingRateSpreadSteps = new IOINoUnderlyingRateSpreadSteps[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingRateSpreadSteps[i] = new();
					((IFixParser)NoUnderlyingRateSpreadSteps[i]).Parse(viewNoUnderlyingRateSpreadSteps.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingRateSpreadSteps":
					value = NoUnderlyingRateSpreadSteps;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingRateSpreadSteps = null;
		}
	}
}
