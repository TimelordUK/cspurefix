using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamFirstPeriodStartDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40974, Offset = 0, Required = false)]
		public NoUnderlyingStreamFirstPeriodStartDateBusinessCenters[]? NoUnderlyingStreamFirstPeriodStartDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamFirstPeriodStartDateBusinessCenters is not null && NoUnderlyingStreamFirstPeriodStartDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40974, NoUnderlyingStreamFirstPeriodStartDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingStreamFirstPeriodStartDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamFirstPeriodStartDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamFirstPeriodStartDateBusinessCenters") is IMessageView viewNoUnderlyingStreamFirstPeriodStartDateBusinessCenters)
			{
				var count = viewNoUnderlyingStreamFirstPeriodStartDateBusinessCenters.GroupCount();
				NoUnderlyingStreamFirstPeriodStartDateBusinessCenters = new NoUnderlyingStreamFirstPeriodStartDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamFirstPeriodStartDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingStreamFirstPeriodStartDateBusinessCenters[i]).Parse(viewNoUnderlyingStreamFirstPeriodStartDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamFirstPeriodStartDateBusinessCenters":
					value = NoUnderlyingStreamFirstPeriodStartDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
