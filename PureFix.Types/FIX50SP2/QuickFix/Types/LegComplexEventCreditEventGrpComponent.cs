using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventCreditEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41366, Offset = 0, Required = false)]
		public IOINoLegComplexEventCreditEvents[]? NoLegComplexEventCreditEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventCreditEvents is not null && NoLegComplexEventCreditEvents.Length != 0)
			{
				writer.WriteWholeNumber(41366, NoLegComplexEventCreditEvents.Length);
				for (int i = 0; i < NoLegComplexEventCreditEvents.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventCreditEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventCreditEvents") is IMessageView viewNoLegComplexEventCreditEvents)
			{
				var count = viewNoLegComplexEventCreditEvents.GroupCount();
				NoLegComplexEventCreditEvents = new IOINoLegComplexEventCreditEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventCreditEvents[i] = new();
					((IFixParser)NoLegComplexEventCreditEvents[i]).Parse(viewNoLegComplexEventCreditEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventCreditEvents":
					value = NoLegComplexEventCreditEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventCreditEvents = null;
		}
	}
}
