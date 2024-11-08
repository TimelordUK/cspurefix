using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCalculationPeriodDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41954, Offset = 0, Required = false)]
		public IOINoUnderlyingStreamCalculationPeriodDates[]? NoUnderlyingStreamCalculationPeriodDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCalculationPeriodDates is not null && NoUnderlyingStreamCalculationPeriodDates.Length != 0)
			{
				writer.WriteWholeNumber(41954, NoUnderlyingStreamCalculationPeriodDates.Length);
				for (int i = 0; i < NoUnderlyingStreamCalculationPeriodDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCalculationPeriodDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCalculationPeriodDates") is IMessageView viewNoUnderlyingStreamCalculationPeriodDates)
			{
				var count = viewNoUnderlyingStreamCalculationPeriodDates.GroupCount();
				NoUnderlyingStreamCalculationPeriodDates = new IOINoUnderlyingStreamCalculationPeriodDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCalculationPeriodDates[i] = new();
					((IFixParser)NoUnderlyingStreamCalculationPeriodDates[i]).Parse(viewNoUnderlyingStreamCalculationPeriodDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCalculationPeriodDates":
					value = NoUnderlyingStreamCalculationPeriodDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamCalculationPeriodDates = null;
		}
	}
}
