using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCalculationPeriodDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41241, Offset = 0, Required = false)]
		public IOINoStreamCalculationPeriodDates[]? NoStreamCalculationPeriodDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCalculationPeriodDates is not null && NoStreamCalculationPeriodDates.Length != 0)
			{
				writer.WriteWholeNumber(41241, NoStreamCalculationPeriodDates.Length);
				for (int i = 0; i < NoStreamCalculationPeriodDates.Length; i++)
				{
					((IFixEncoder)NoStreamCalculationPeriodDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCalculationPeriodDates") is IMessageView viewNoStreamCalculationPeriodDates)
			{
				var count = viewNoStreamCalculationPeriodDates.GroupCount();
				NoStreamCalculationPeriodDates = new IOINoStreamCalculationPeriodDates[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCalculationPeriodDates[i] = new();
					((IFixParser)NoStreamCalculationPeriodDates[i]).Parse(viewNoStreamCalculationPeriodDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCalculationPeriodDates":
					value = NoStreamCalculationPeriodDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStreamCalculationPeriodDates = null;
		}
	}
}
