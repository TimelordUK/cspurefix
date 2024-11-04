using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventsComponent : IFixComponent
	{
		[Group(NoOfTag = 2218, Offset = 0, Required = false)]
		public NoLegComplexEvents[]? NoLegComplexEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEvents is not null && NoLegComplexEvents.Length != 0)
			{
				writer.WriteWholeNumber(2218, NoLegComplexEvents.Length);
				for (int i = 0; i < NoLegComplexEvents.Length; i++)
				{
					((IFixEncoder)NoLegComplexEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEvents") is IMessageView viewNoLegComplexEvents)
			{
				var count = viewNoLegComplexEvents.GroupCount();
				NoLegComplexEvents = new NoLegComplexEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEvents[i] = new();
					((IFixParser)NoLegComplexEvents[i]).Parse(viewNoLegComplexEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEvents":
					value = NoLegComplexEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEvents = null;
		}
	}
}
