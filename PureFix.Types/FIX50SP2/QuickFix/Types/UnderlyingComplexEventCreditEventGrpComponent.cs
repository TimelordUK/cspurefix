using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventCreditEventGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41716, Offset = 0, Required = false)]
		public NoUnderlyingComplexEventCreditEvents[]? NoUnderlyingComplexEventCreditEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventCreditEvents is not null && NoUnderlyingComplexEventCreditEvents.Length != 0)
			{
				writer.WriteWholeNumber(41716, NoUnderlyingComplexEventCreditEvents.Length);
				for (int i = 0; i < NoUnderlyingComplexEventCreditEvents.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventCreditEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventCreditEvents") is IMessageView viewNoUnderlyingComplexEventCreditEvents)
			{
				var count = viewNoUnderlyingComplexEventCreditEvents.GroupCount();
				NoUnderlyingComplexEventCreditEvents = new NoUnderlyingComplexEventCreditEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventCreditEvents[i] = new();
					((IFixParser)NoUnderlyingComplexEventCreditEvents[i]).Parse(viewNoUnderlyingComplexEventCreditEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventCreditEvents":
					value = NoUnderlyingComplexEventCreditEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingComplexEventCreditEvents = null;
		}
	}
}
