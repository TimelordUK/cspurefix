using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventTimesComponent : IFixComponent
	{
		[Group(NoOfTag = 2056, Offset = 0, Required = false)]
		public NoUnderlyingComplexEventTimes[]? NoUnderlyingComplexEventTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventTimes is not null && NoUnderlyingComplexEventTimes.Length != 0)
			{
				writer.WriteWholeNumber(2056, NoUnderlyingComplexEventTimes.Length);
				for (int i = 0; i < NoUnderlyingComplexEventTimes.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventTimes") is IMessageView viewNoUnderlyingComplexEventTimes)
			{
				var count = viewNoUnderlyingComplexEventTimes.GroupCount();
				NoUnderlyingComplexEventTimes = new NoUnderlyingComplexEventTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventTimes[i] = new();
					((IFixParser)NoUnderlyingComplexEventTimes[i]).Parse(viewNoUnderlyingComplexEventTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventTimes":
					value = NoUnderlyingComplexEventTimes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
