using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventsComponent : IFixComponent
	{
		[Group(NoOfTag = 1483, Offset = 0, Required = false)]
		public IOINoComplexEvents[]? NoComplexEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEvents is not null && NoComplexEvents.Length != 0)
			{
				writer.WriteWholeNumber(1483, NoComplexEvents.Length);
				for (int i = 0; i < NoComplexEvents.Length; i++)
				{
					((IFixEncoder)NoComplexEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEvents") is IMessageView viewNoComplexEvents)
			{
				var count = viewNoComplexEvents.GroupCount();
				NoComplexEvents = new IOINoComplexEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEvents[i] = new();
					((IFixParser)NoComplexEvents[i]).Parse(viewNoComplexEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEvents":
					value = NoComplexEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEvents = null;
		}
	}
}
