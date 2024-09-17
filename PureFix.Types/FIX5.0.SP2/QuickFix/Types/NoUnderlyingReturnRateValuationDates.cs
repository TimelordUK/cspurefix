using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingReturnRateValuationDates : IFixGroup
	{
		[TagDetails(Tag = 43072, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? UnderlyingReturnRateValuationDate {get; set;}
		
		[TagDetails(Tag = 43073, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingReturnRateValuationDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingReturnRateValuationDate is not null) writer.WriteLocalDateOnly(43072, UnderlyingReturnRateValuationDate.Value);
			if (UnderlyingReturnRateValuationDateType is not null) writer.WriteWholeNumber(43073, UnderlyingReturnRateValuationDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingReturnRateValuationDate = view.GetDateOnly(43072);
			UnderlyingReturnRateValuationDateType = view.GetInt32(43073);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingReturnRateValuationDate":
					value = UnderlyingReturnRateValuationDate;
					break;
				case "UnderlyingReturnRateValuationDateType":
					value = UnderlyingReturnRateValuationDateType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
