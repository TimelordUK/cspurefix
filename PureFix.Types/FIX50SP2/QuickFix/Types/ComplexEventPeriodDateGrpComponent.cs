using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventPeriodDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41007, Offset = 0, Required = false)]
		public NoComplexEventPeriodDateTimes[]? NoComplexEventPeriodDateTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventPeriodDateTimes is not null && NoComplexEventPeriodDateTimes.Length != 0)
			{
				writer.WriteWholeNumber(41007, NoComplexEventPeriodDateTimes.Length);
				for (int i = 0; i < NoComplexEventPeriodDateTimes.Length; i++)
				{
					((IFixEncoder)NoComplexEventPeriodDateTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventPeriodDateTimes") is IMessageView viewNoComplexEventPeriodDateTimes)
			{
				var count = viewNoComplexEventPeriodDateTimes.GroupCount();
				NoComplexEventPeriodDateTimes = new NoComplexEventPeriodDateTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventPeriodDateTimes[i] = new();
					((IFixParser)NoComplexEventPeriodDateTimes[i]).Parse(viewNoComplexEventPeriodDateTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventPeriodDateTimes":
					value = NoComplexEventPeriodDateTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventPeriodDateTimes = null;
		}
	}
}
