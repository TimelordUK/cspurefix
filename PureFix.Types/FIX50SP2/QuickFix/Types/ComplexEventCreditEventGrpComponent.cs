using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventCreditEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40997, Offset = 0, Required = false)]
		public IOINoComplexEventCreditEvents[]? NoComplexEventCreditEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventCreditEvents is not null && NoComplexEventCreditEvents.Length != 0)
			{
				writer.WriteWholeNumber(40997, NoComplexEventCreditEvents.Length);
				for (int i = 0; i < NoComplexEventCreditEvents.Length; i++)
				{
					((IFixEncoder)NoComplexEventCreditEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventCreditEvents") is IMessageView viewNoComplexEventCreditEvents)
			{
				var count = viewNoComplexEventCreditEvents.GroupCount();
				NoComplexEventCreditEvents = new IOINoComplexEventCreditEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventCreditEvents[i] = new();
					((IFixParser)NoComplexEventCreditEvents[i]).Parse(viewNoComplexEventCreditEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventCreditEvents":
					value = NoComplexEventCreditEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventCreditEvents = null;
		}
	}
}
