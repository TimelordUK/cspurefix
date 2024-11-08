using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoOptionExerciseDates : IFixGroup
	{
		[TagDetails(Tag = 41138, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? OptionExerciseDate {get; set;}
		
		[TagDetails(Tag = 41139, Type = TagType.Int, Offset = 1, Required = false)]
		public int? OptionExerciseDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OptionExerciseDate is not null) writer.WriteLocalDateOnly(41138, OptionExerciseDate.Value);
			if (OptionExerciseDateType is not null) writer.WriteWholeNumber(41139, OptionExerciseDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OptionExerciseDate = view.GetDateOnly(41138);
			OptionExerciseDateType = view.GetInt32(41139);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "OptionExerciseDate":
					value = OptionExerciseDate;
					break;
				case "OptionExerciseDateType":
					value = OptionExerciseDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			OptionExerciseDate = null;
			OptionExerciseDateType = null;
		}
	}
}
