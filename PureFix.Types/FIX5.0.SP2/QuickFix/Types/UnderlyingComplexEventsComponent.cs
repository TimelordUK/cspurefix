using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventsComponent : IFixComponent
	{
		[Group(NoOfTag = 2045, Offset = 0, Required = false)]
		public NoUnderlyingComplexEvents[]? NoUnderlyingComplexEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEvents is not null && NoUnderlyingComplexEvents.Length != 0)
			{
				writer.WriteWholeNumber(2045, NoUnderlyingComplexEvents.Length);
				for (int i = 0; i < NoUnderlyingComplexEvents.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEvents") is IMessageView viewNoUnderlyingComplexEvents)
			{
				var count = viewNoUnderlyingComplexEvents.GroupCount();
				NoUnderlyingComplexEvents = new NoUnderlyingComplexEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEvents[i] = new();
					((IFixParser)NoUnderlyingComplexEvents[i]).Parse(viewNoUnderlyingComplexEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEvents":
					value = NoUnderlyingComplexEvents;
					break;
				default: return false;
			}
			return true;
		}
	}
}
