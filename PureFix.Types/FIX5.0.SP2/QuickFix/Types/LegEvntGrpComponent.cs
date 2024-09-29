using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegEvntGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2059, Offset = 0, Required = false)]
		public NoLegEvents[]? NoLegEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegEvents is not null && NoLegEvents.Length != 0)
			{
				writer.WriteWholeNumber(2059, NoLegEvents.Length);
				for (int i = 0; i < NoLegEvents.Length; i++)
				{
					((IFixEncoder)NoLegEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegEvents") is IMessageView viewNoLegEvents)
			{
				var count = viewNoLegEvents.GroupCount();
				NoLegEvents = new NoLegEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoLegEvents[i] = new();
					((IFixParser)NoLegEvents[i]).Parse(viewNoLegEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegEvents":
					value = NoLegEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegEvents = null;
		}
	}
}
