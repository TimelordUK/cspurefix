using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProtectionTermEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41625, Offset = 0, Required = false)]
		public IOINoLegProtectionTermEvents[]? NoLegProtectionTermEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProtectionTermEvents is not null && NoLegProtectionTermEvents.Length != 0)
			{
				writer.WriteWholeNumber(41625, NoLegProtectionTermEvents.Length);
				for (int i = 0; i < NoLegProtectionTermEvents.Length; i++)
				{
					((IFixEncoder)NoLegProtectionTermEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProtectionTermEvents") is IMessageView viewNoLegProtectionTermEvents)
			{
				var count = viewNoLegProtectionTermEvents.GroupCount();
				NoLegProtectionTermEvents = new IOINoLegProtectionTermEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProtectionTermEvents[i] = new();
					((IFixParser)NoLegProtectionTermEvents[i]).Parse(viewNoLegProtectionTermEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProtectionTermEvents":
					value = NoLegProtectionTermEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProtectionTermEvents = null;
		}
	}
}
