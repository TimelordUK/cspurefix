using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventTimesComponent : IFixComponent
	{
		[Group(NoOfTag = 2253, Offset = 0, Required = false)]
		public NoLegComplexEventTimes[]? NoLegComplexEventTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventTimes is not null && NoLegComplexEventTimes.Length != 0)
			{
				writer.WriteWholeNumber(2253, NoLegComplexEventTimes.Length);
				for (int i = 0; i < NoLegComplexEventTimes.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventTimes") is IMessageView viewNoLegComplexEventTimes)
			{
				var count = viewNoLegComplexEventTimes.GroupCount();
				NoLegComplexEventTimes = new NoLegComplexEventTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventTimes[i] = new();
					((IFixParser)NoLegComplexEventTimes[i]).Parse(viewNoLegComplexEventTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventTimes":
					value = NoLegComplexEventTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventTimes = null;
		}
	}
}
