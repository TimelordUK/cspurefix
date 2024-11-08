using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41379, Offset = 0, Required = false)]
		public IOINoLegComplexEventPeriods[]? NoLegComplexEventPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventPeriods is not null && NoLegComplexEventPeriods.Length != 0)
			{
				writer.WriteWholeNumber(41379, NoLegComplexEventPeriods.Length);
				for (int i = 0; i < NoLegComplexEventPeriods.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventPeriods") is IMessageView viewNoLegComplexEventPeriods)
			{
				var count = viewNoLegComplexEventPeriods.GroupCount();
				NoLegComplexEventPeriods = new IOINoLegComplexEventPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventPeriods[i] = new();
					((IFixParser)NoLegComplexEventPeriods[i]).Parse(viewNoLegComplexEventPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventPeriods":
					value = NoLegComplexEventPeriods;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventPeriods = null;
		}
	}
}
