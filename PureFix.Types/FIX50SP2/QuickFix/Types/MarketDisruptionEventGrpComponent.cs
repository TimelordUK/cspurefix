using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarketDisruptionEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41092, Offset = 0, Required = false)]
		public IOINoMarketDisruptionEvents[]? NoMarketDisruptionEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMarketDisruptionEvents is not null && NoMarketDisruptionEvents.Length != 0)
			{
				writer.WriteWholeNumber(41092, NoMarketDisruptionEvents.Length);
				for (int i = 0; i < NoMarketDisruptionEvents.Length; i++)
				{
					((IFixEncoder)NoMarketDisruptionEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMarketDisruptionEvents") is IMessageView viewNoMarketDisruptionEvents)
			{
				var count = viewNoMarketDisruptionEvents.GroupCount();
				NoMarketDisruptionEvents = new IOINoMarketDisruptionEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoMarketDisruptionEvents[i] = new();
					((IFixParser)NoMarketDisruptionEvents[i]).Parse(viewNoMarketDisruptionEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMarketDisruptionEvents":
					value = NoMarketDisruptionEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMarketDisruptionEvents = null;
		}
	}
}
