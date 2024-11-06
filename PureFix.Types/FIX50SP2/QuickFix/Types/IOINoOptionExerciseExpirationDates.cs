using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoOptionExerciseExpirationDates : IFixGroup
	{
		[TagDetails(Tag = 41153, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? OptionExerciseExpirationDate {get; set;}
		
		[TagDetails(Tag = 41154, Type = TagType.Int, Offset = 1, Required = false)]
		public int? OptionExerciseExpirationDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OptionExerciseExpirationDate is not null) writer.WriteLocalDateOnly(41153, OptionExerciseExpirationDate.Value);
			if (OptionExerciseExpirationDateType is not null) writer.WriteWholeNumber(41154, OptionExerciseExpirationDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OptionExerciseExpirationDate = view.GetDateOnly(41153);
			OptionExerciseExpirationDateType = view.GetInt32(41154);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "OptionExerciseExpirationDate":
					value = OptionExerciseExpirationDate;
					break;
				case "OptionExerciseExpirationDateType":
					value = OptionExerciseExpirationDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			OptionExerciseExpirationDate = null;
			OptionExerciseExpirationDateType = null;
		}
	}
}
