using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventPeriodDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41376, Offset = 0, Required = false)]
		public IOINoLegComplexEventPeriodDateTimes[]? NoLegComplexEventPeriodDateTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventPeriodDateTimes is not null && NoLegComplexEventPeriodDateTimes.Length != 0)
			{
				writer.WriteWholeNumber(41376, NoLegComplexEventPeriodDateTimes.Length);
				for (int i = 0; i < NoLegComplexEventPeriodDateTimes.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventPeriodDateTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventPeriodDateTimes") is IMessageView viewNoLegComplexEventPeriodDateTimes)
			{
				var count = viewNoLegComplexEventPeriodDateTimes.GroupCount();
				NoLegComplexEventPeriodDateTimes = new IOINoLegComplexEventPeriodDateTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventPeriodDateTimes[i] = new();
					((IFixParser)NoLegComplexEventPeriodDateTimes[i]).Parse(viewNoLegComplexEventPeriodDateTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventPeriodDateTimes":
					value = NoLegComplexEventPeriodDateTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventPeriodDateTimes = null;
		}
	}
}
