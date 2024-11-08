using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamFirstPeriodStartDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40941, Offset = 0, Required = false)]
		public IOINoLegStreamFirstPeriodStartDateBusinessCenters[]? NoLegStreamFirstPeriodStartDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamFirstPeriodStartDateBusinessCenters is not null && NoLegStreamFirstPeriodStartDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40941, NoLegStreamFirstPeriodStartDateBusinessCenters.Length);
				for (int i = 0; i < NoLegStreamFirstPeriodStartDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegStreamFirstPeriodStartDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamFirstPeriodStartDateBusinessCenters") is IMessageView viewNoLegStreamFirstPeriodStartDateBusinessCenters)
			{
				var count = viewNoLegStreamFirstPeriodStartDateBusinessCenters.GroupCount();
				NoLegStreamFirstPeriodStartDateBusinessCenters = new IOINoLegStreamFirstPeriodStartDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamFirstPeriodStartDateBusinessCenters[i] = new();
					((IFixParser)NoLegStreamFirstPeriodStartDateBusinessCenters[i]).Parse(viewNoLegStreamFirstPeriodStartDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamFirstPeriodStartDateBusinessCenters":
					value = NoLegStreamFirstPeriodStartDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamFirstPeriodStartDateBusinessCenters = null;
		}
	}
}
