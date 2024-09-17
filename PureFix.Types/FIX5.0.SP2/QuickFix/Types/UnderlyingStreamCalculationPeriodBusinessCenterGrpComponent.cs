using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCalculationPeriodBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40973, Offset = 0, Required = false)]
		public NoUnderlyingStreamCalculationPeriodBusinessCenters[]? NoUnderlyingStreamCalculationPeriodBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCalculationPeriodBusinessCenters is not null && NoUnderlyingStreamCalculationPeriodBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40973, NoUnderlyingStreamCalculationPeriodBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingStreamCalculationPeriodBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCalculationPeriodBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCalculationPeriodBusinessCenters") is IMessageView viewNoUnderlyingStreamCalculationPeriodBusinessCenters)
			{
				var count = viewNoUnderlyingStreamCalculationPeriodBusinessCenters.GroupCount();
				NoUnderlyingStreamCalculationPeriodBusinessCenters = new NoUnderlyingStreamCalculationPeriodBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCalculationPeriodBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingStreamCalculationPeriodBusinessCenters[i]).Parse(viewNoUnderlyingStreamCalculationPeriodBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCalculationPeriodBusinessCenters":
					value = NoUnderlyingStreamCalculationPeriodBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
