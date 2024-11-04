using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingExtraordinaryEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42884, Offset = 0, Required = false)]
		public NoUnderlyingExtraordinaryEvents[]? NoUnderlyingExtraordinaryEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingExtraordinaryEvents is not null && NoUnderlyingExtraordinaryEvents.Length != 0)
			{
				writer.WriteWholeNumber(42884, NoUnderlyingExtraordinaryEvents.Length);
				for (int i = 0; i < NoUnderlyingExtraordinaryEvents.Length; i++)
				{
					((IFixEncoder)NoUnderlyingExtraordinaryEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingExtraordinaryEvents") is IMessageView viewNoUnderlyingExtraordinaryEvents)
			{
				var count = viewNoUnderlyingExtraordinaryEvents.GroupCount();
				NoUnderlyingExtraordinaryEvents = new NoUnderlyingExtraordinaryEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingExtraordinaryEvents[i] = new();
					((IFixParser)NoUnderlyingExtraordinaryEvents[i]).Parse(viewNoUnderlyingExtraordinaryEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingExtraordinaryEvents":
					value = NoUnderlyingExtraordinaryEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingExtraordinaryEvents = null;
		}
	}
}
