using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamFirstPeriodStartDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40959, Offset = 0, Required = false)]
		public IOINoStreamFirstPeriodStartDateBusinessCenters[]? NoStreamFirstPeriodStartDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamFirstPeriodStartDateBusinessCenters is not null && NoStreamFirstPeriodStartDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40959, NoStreamFirstPeriodStartDateBusinessCenters.Length);
				for (int i = 0; i < NoStreamFirstPeriodStartDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoStreamFirstPeriodStartDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamFirstPeriodStartDateBusinessCenters") is IMessageView viewNoStreamFirstPeriodStartDateBusinessCenters)
			{
				var count = viewNoStreamFirstPeriodStartDateBusinessCenters.GroupCount();
				NoStreamFirstPeriodStartDateBusinessCenters = new IOINoStreamFirstPeriodStartDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamFirstPeriodStartDateBusinessCenters[i] = new();
					((IFixParser)NoStreamFirstPeriodStartDateBusinessCenters[i]).Parse(viewNoStreamFirstPeriodStartDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamFirstPeriodStartDateBusinessCenters":
					value = NoStreamFirstPeriodStartDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStreamFirstPeriodStartDateBusinessCenters = null;
		}
	}
}
