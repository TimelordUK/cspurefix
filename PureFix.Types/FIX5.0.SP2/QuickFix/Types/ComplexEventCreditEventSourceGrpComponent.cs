using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventCreditEventSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41029, Offset = 0, Required = false)]
		public NoComplexEventCreditEventSources[]? NoComplexEventCreditEventSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventCreditEventSources is not null && NoComplexEventCreditEventSources.Length != 0)
			{
				writer.WriteWholeNumber(41029, NoComplexEventCreditEventSources.Length);
				for (int i = 0; i < NoComplexEventCreditEventSources.Length; i++)
				{
					((IFixEncoder)NoComplexEventCreditEventSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventCreditEventSources") is IMessageView viewNoComplexEventCreditEventSources)
			{
				var count = viewNoComplexEventCreditEventSources.GroupCount();
				NoComplexEventCreditEventSources = new NoComplexEventCreditEventSources[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventCreditEventSources[i] = new();
					((IFixParser)NoComplexEventCreditEventSources[i]).Parse(viewNoComplexEventCreditEventSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventCreditEventSources":
					value = NoComplexEventCreditEventSources;
					break;
				default: return false;
			}
			return true;
		}
	}
}
