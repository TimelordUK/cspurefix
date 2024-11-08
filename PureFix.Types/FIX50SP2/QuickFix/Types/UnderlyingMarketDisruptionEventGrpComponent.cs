using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingMarketDisruptionEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41864, Offset = 0, Required = false)]
		public IOINoUnderlyingMarketDisruptionEvents[]? NoUnderlyingMarketDisruptionEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingMarketDisruptionEvents is not null && NoUnderlyingMarketDisruptionEvents.Length != 0)
			{
				writer.WriteWholeNumber(41864, NoUnderlyingMarketDisruptionEvents.Length);
				for (int i = 0; i < NoUnderlyingMarketDisruptionEvents.Length; i++)
				{
					((IFixEncoder)NoUnderlyingMarketDisruptionEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingMarketDisruptionEvents") is IMessageView viewNoUnderlyingMarketDisruptionEvents)
			{
				var count = viewNoUnderlyingMarketDisruptionEvents.GroupCount();
				NoUnderlyingMarketDisruptionEvents = new IOINoUnderlyingMarketDisruptionEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingMarketDisruptionEvents[i] = new();
					((IFixParser)NoUnderlyingMarketDisruptionEvents[i]).Parse(viewNoUnderlyingMarketDisruptionEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingMarketDisruptionEvents":
					value = NoUnderlyingMarketDisruptionEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingMarketDisruptionEvents = null;
		}
	}
}
