using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingStreamCalculationPeriodDates : IFixGroup
	{
		[TagDetails(Tag = 41955, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? UnderlyingStreamCalculationPeriodDate {get; set;}
		
		[TagDetails(Tag = 41956, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingStreamCalculationPeriodDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStreamCalculationPeriodDate is not null) writer.WriteLocalDateOnly(41955, UnderlyingStreamCalculationPeriodDate.Value);
			if (UnderlyingStreamCalculationPeriodDateType is not null) writer.WriteWholeNumber(41956, UnderlyingStreamCalculationPeriodDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStreamCalculationPeriodDate = view.GetDateOnly(41955);
			UnderlyingStreamCalculationPeriodDateType = view.GetInt32(41956);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStreamCalculationPeriodDate":
					value = UnderlyingStreamCalculationPeriodDate;
					break;
				case "UnderlyingStreamCalculationPeriodDateType":
					value = UnderlyingStreamCalculationPeriodDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingStreamCalculationPeriodDate = null;
			UnderlyingStreamCalculationPeriodDateType = null;
		}
	}
}
