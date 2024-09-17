using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExtraordinaryEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42296, Offset = 0, Required = false)]
		public NoExtraordinaryEvents[]? NoExtraordinaryEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoExtraordinaryEvents is not null && NoExtraordinaryEvents.Length != 0)
			{
				writer.WriteWholeNumber(42296, NoExtraordinaryEvents.Length);
				for (int i = 0; i < NoExtraordinaryEvents.Length; i++)
				{
					((IFixEncoder)NoExtraordinaryEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoExtraordinaryEvents") is IMessageView viewNoExtraordinaryEvents)
			{
				var count = viewNoExtraordinaryEvents.GroupCount();
				NoExtraordinaryEvents = new NoExtraordinaryEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoExtraordinaryEvents[i] = new();
					((IFixParser)NoExtraordinaryEvents[i]).Parse(viewNoExtraordinaryEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoExtraordinaryEvents":
					value = NoExtraordinaryEvents;
					break;
				default: return false;
			}
			return true;
		}
	}
}
