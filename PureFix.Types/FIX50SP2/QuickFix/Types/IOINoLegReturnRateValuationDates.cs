using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegReturnRateValuationDates : IFixGroup
	{
		[TagDetails(Tag = 42572, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegReturnRateValuationDate {get; set;}
		
		[TagDetails(Tag = 42573, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegReturnRateValuationDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegReturnRateValuationDate is not null) writer.WriteLocalDateOnly(42572, LegReturnRateValuationDate.Value);
			if (LegReturnRateValuationDateType is not null) writer.WriteWholeNumber(42573, LegReturnRateValuationDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegReturnRateValuationDate = view.GetDateOnly(42572);
			LegReturnRateValuationDateType = view.GetInt32(42573);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegReturnRateValuationDate":
					value = LegReturnRateValuationDate;
					break;
				case "LegReturnRateValuationDateType":
					value = LegReturnRateValuationDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegReturnRateValuationDate = null;
			LegReturnRateValuationDateType = null;
		}
	}
}
