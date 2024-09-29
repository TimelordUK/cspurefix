using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegStreamCalculationPeriodDates : IFixGroup
	{
		[TagDetails(Tag = 41639, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegStreamCalculationPeriodDate {get; set;}
		
		[TagDetails(Tag = 41640, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegStreamCalculationPeriodDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegStreamCalculationPeriodDate is not null) writer.WriteLocalDateOnly(41639, LegStreamCalculationPeriodDate.Value);
			if (LegStreamCalculationPeriodDateType is not null) writer.WriteWholeNumber(41640, LegStreamCalculationPeriodDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegStreamCalculationPeriodDate = view.GetDateOnly(41639);
			LegStreamCalculationPeriodDateType = view.GetInt32(41640);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegStreamCalculationPeriodDate":
					value = LegStreamCalculationPeriodDate;
					break;
				case "LegStreamCalculationPeriodDateType":
					value = LegStreamCalculationPeriodDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegStreamCalculationPeriodDate = null;
			LegStreamCalculationPeriodDateType = null;
		}
	}
}
