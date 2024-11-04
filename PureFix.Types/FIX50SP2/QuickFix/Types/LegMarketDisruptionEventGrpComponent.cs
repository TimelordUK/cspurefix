using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegMarketDisruptionEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41467, Offset = 0, Required = false)]
		public NoLegMarketDisruptionEvents[]? NoLegMarketDisruptionEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegMarketDisruptionEvents is not null && NoLegMarketDisruptionEvents.Length != 0)
			{
				writer.WriteWholeNumber(41467, NoLegMarketDisruptionEvents.Length);
				for (int i = 0; i < NoLegMarketDisruptionEvents.Length; i++)
				{
					((IFixEncoder)NoLegMarketDisruptionEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegMarketDisruptionEvents") is IMessageView viewNoLegMarketDisruptionEvents)
			{
				var count = viewNoLegMarketDisruptionEvents.GroupCount();
				NoLegMarketDisruptionEvents = new NoLegMarketDisruptionEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoLegMarketDisruptionEvents[i] = new();
					((IFixParser)NoLegMarketDisruptionEvents[i]).Parse(viewNoLegMarketDisruptionEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegMarketDisruptionEvents":
					value = NoLegMarketDisruptionEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegMarketDisruptionEvents = null;
		}
	}
}
