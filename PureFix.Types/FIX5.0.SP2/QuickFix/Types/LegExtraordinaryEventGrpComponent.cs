using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegExtraordinaryEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42388, Offset = 0, Required = false)]
		public NoLegExtraordinaryEvents[]? NoLegExtraordinaryEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegExtraordinaryEvents is not null && NoLegExtraordinaryEvents.Length != 0)
			{
				writer.WriteWholeNumber(42388, NoLegExtraordinaryEvents.Length);
				for (int i = 0; i < NoLegExtraordinaryEvents.Length; i++)
				{
					((IFixEncoder)NoLegExtraordinaryEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegExtraordinaryEvents") is IMessageView viewNoLegExtraordinaryEvents)
			{
				var count = viewNoLegExtraordinaryEvents.GroupCount();
				NoLegExtraordinaryEvents = new NoLegExtraordinaryEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoLegExtraordinaryEvents[i] = new();
					((IFixParser)NoLegExtraordinaryEvents[i]).Parse(viewNoLegExtraordinaryEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegExtraordinaryEvents":
					value = NoLegExtraordinaryEvents;
					break;
				default: return false;
			}
			return true;
		}
	}
}
