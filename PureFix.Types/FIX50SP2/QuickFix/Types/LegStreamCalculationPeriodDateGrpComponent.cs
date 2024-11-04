using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamCalculationPeriodDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41638, Offset = 0, Required = false)]
		public NoLegStreamCalculationPeriodDates[]? NoLegStreamCalculationPeriodDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamCalculationPeriodDates is not null && NoLegStreamCalculationPeriodDates.Length != 0)
			{
				writer.WriteWholeNumber(41638, NoLegStreamCalculationPeriodDates.Length);
				for (int i = 0; i < NoLegStreamCalculationPeriodDates.Length; i++)
				{
					((IFixEncoder)NoLegStreamCalculationPeriodDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamCalculationPeriodDates") is IMessageView viewNoLegStreamCalculationPeriodDates)
			{
				var count = viewNoLegStreamCalculationPeriodDates.GroupCount();
				NoLegStreamCalculationPeriodDates = new NoLegStreamCalculationPeriodDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamCalculationPeriodDates[i] = new();
					((IFixParser)NoLegStreamCalculationPeriodDates[i]).Parse(viewNoLegStreamCalculationPeriodDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamCalculationPeriodDates":
					value = NoLegStreamCalculationPeriodDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamCalculationPeriodDates = null;
		}
	}
}
