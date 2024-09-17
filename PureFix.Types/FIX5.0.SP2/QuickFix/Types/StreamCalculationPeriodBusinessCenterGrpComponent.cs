using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCalculationPeriodBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40958, Offset = 0, Required = false)]
		public NoStreamCalculationPeriodBusinessCenters[]? NoStreamCalculationPeriodBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCalculationPeriodBusinessCenters is not null && NoStreamCalculationPeriodBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40958, NoStreamCalculationPeriodBusinessCenters.Length);
				for (int i = 0; i < NoStreamCalculationPeriodBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoStreamCalculationPeriodBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCalculationPeriodBusinessCenters") is IMessageView viewNoStreamCalculationPeriodBusinessCenters)
			{
				var count = viewNoStreamCalculationPeriodBusinessCenters.GroupCount();
				NoStreamCalculationPeriodBusinessCenters = new NoStreamCalculationPeriodBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCalculationPeriodBusinessCenters[i] = new();
					((IFixParser)NoStreamCalculationPeriodBusinessCenters[i]).Parse(viewNoStreamCalculationPeriodBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCalculationPeriodBusinessCenters":
					value = NoStreamCalculationPeriodBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
