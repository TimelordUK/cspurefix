using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoReturnRateValuationDates : IFixGroup
	{
		[TagDetails(Tag = 42773, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? ReturnRateValuationDate {get; set;}
		
		[TagDetails(Tag = 42774, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ReturnRateValuationDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ReturnRateValuationDate is not null) writer.WriteLocalDateOnly(42773, ReturnRateValuationDate.Value);
			if (ReturnRateValuationDateType is not null) writer.WriteWholeNumber(42774, ReturnRateValuationDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ReturnRateValuationDate = view.GetDateOnly(42773);
			ReturnRateValuationDateType = view.GetInt32(42774);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ReturnRateValuationDate":
					value = ReturnRateValuationDate;
					break;
				case "ReturnRateValuationDateType":
					value = ReturnRateValuationDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ReturnRateValuationDate = null;
			ReturnRateValuationDateType = null;
		}
	}
}
