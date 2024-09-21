using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventPeriodDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41726, Offset = 0, Required = false)]
		public NoUnderlyingComplexEventPeriodDateTimes[]? NoUnderlyingComplexEventPeriodDateTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventPeriodDateTimes is not null && NoUnderlyingComplexEventPeriodDateTimes.Length != 0)
			{
				writer.WriteWholeNumber(41726, NoUnderlyingComplexEventPeriodDateTimes.Length);
				for (int i = 0; i < NoUnderlyingComplexEventPeriodDateTimes.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventPeriodDateTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventPeriodDateTimes") is IMessageView viewNoUnderlyingComplexEventPeriodDateTimes)
			{
				var count = viewNoUnderlyingComplexEventPeriodDateTimes.GroupCount();
				NoUnderlyingComplexEventPeriodDateTimes = new NoUnderlyingComplexEventPeriodDateTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventPeriodDateTimes[i] = new();
					((IFixParser)NoUnderlyingComplexEventPeriodDateTimes[i]).Parse(viewNoUnderlyingComplexEventPeriodDateTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventPeriodDateTimes":
					value = NoUnderlyingComplexEventPeriodDateTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingComplexEventPeriodDateTimes = null;
		}
	}
}
