using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class EvntGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 864, Offset = 0, Required = false)]
		public IOINoEvents[]? NoEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoEvents is not null && NoEvents.Length != 0)
			{
				writer.WriteWholeNumber(864, NoEvents.Length);
				for (int i = 0; i < NoEvents.Length; i++)
				{
					((IFixEncoder)NoEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoEvents") is IMessageView viewNoEvents)
			{
				var count = viewNoEvents.GroupCount();
				NoEvents = new IOINoEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoEvents[i] = new();
					((IFixParser)NoEvents[i]).Parse(viewNoEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoEvents":
					value = NoEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoEvents = null;
		}
	}
}
