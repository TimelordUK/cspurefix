using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingEvntGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1981, Offset = 0, Required = false)]
		public NoUnderlyingEvents[]? NoUnderlyingEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingEvents is not null && NoUnderlyingEvents.Length != 0)
			{
				writer.WriteWholeNumber(1981, NoUnderlyingEvents.Length);
				for (int i = 0; i < NoUnderlyingEvents.Length; i++)
				{
					((IFixEncoder)NoUnderlyingEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingEvents") is IMessageView viewNoUnderlyingEvents)
			{
				var count = viewNoUnderlyingEvents.GroupCount();
				NoUnderlyingEvents = new NoUnderlyingEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingEvents[i] = new();
					((IFixParser)NoUnderlyingEvents[i]).Parse(viewNoUnderlyingEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingEvents":
					value = NoUnderlyingEvents;
					break;
				default: return false;
			}
			return true;
		}
	}
}
