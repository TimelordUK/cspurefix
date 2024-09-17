using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProtectionTermEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40191, Offset = 0, Required = false)]
		public NoProtectionTermEvents[]? NoProtectionTermEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProtectionTermEvents is not null && NoProtectionTermEvents.Length != 0)
			{
				writer.WriteWholeNumber(40191, NoProtectionTermEvents.Length);
				for (int i = 0; i < NoProtectionTermEvents.Length; i++)
				{
					((IFixEncoder)NoProtectionTermEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProtectionTermEvents") is IMessageView viewNoProtectionTermEvents)
			{
				var count = viewNoProtectionTermEvents.GroupCount();
				NoProtectionTermEvents = new NoProtectionTermEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoProtectionTermEvents[i] = new();
					((IFixParser)NoProtectionTermEvents[i]).Parse(viewNoProtectionTermEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProtectionTermEvents":
					value = NoProtectionTermEvents;
					break;
				default: return false;
			}
			return true;
		}
	}
}
