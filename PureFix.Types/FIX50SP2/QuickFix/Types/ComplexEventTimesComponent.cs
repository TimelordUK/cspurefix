using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventTimesComponent : IFixComponent
	{
		[Group(NoOfTag = 1494, Offset = 0, Required = false)]
		public IOINoComplexEventTimes[]? NoComplexEventTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventTimes is not null && NoComplexEventTimes.Length != 0)
			{
				writer.WriteWholeNumber(1494, NoComplexEventTimes.Length);
				for (int i = 0; i < NoComplexEventTimes.Length; i++)
				{
					((IFixEncoder)NoComplexEventTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventTimes") is IMessageView viewNoComplexEventTimes)
			{
				var count = viewNoComplexEventTimes.GroupCount();
				NoComplexEventTimes = new IOINoComplexEventTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventTimes[i] = new();
					((IFixParser)NoComplexEventTimes[i]).Parse(viewNoComplexEventTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventTimes":
					value = NoComplexEventTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventTimes = null;
		}
	}
}
