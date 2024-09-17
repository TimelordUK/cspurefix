using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventCreditEventSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41398, Offset = 0, Required = false)]
		public NoLegComplexEventCreditEventSources[]? NoLegComplexEventCreditEventSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventCreditEventSources is not null && NoLegComplexEventCreditEventSources.Length != 0)
			{
				writer.WriteWholeNumber(41398, NoLegComplexEventCreditEventSources.Length);
				for (int i = 0; i < NoLegComplexEventCreditEventSources.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventCreditEventSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventCreditEventSources") is IMessageView viewNoLegComplexEventCreditEventSources)
			{
				var count = viewNoLegComplexEventCreditEventSources.GroupCount();
				NoLegComplexEventCreditEventSources = new NoLegComplexEventCreditEventSources[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventCreditEventSources[i] = new();
					((IFixParser)NoLegComplexEventCreditEventSources[i]).Parse(viewNoLegComplexEventCreditEventSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventCreditEventSources":
					value = NoLegComplexEventCreditEventSources;
					break;
				default: return false;
			}
			return true;
		}
	}
}
