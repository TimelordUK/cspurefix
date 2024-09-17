using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingOptionExerciseDates : IFixGroup
	{
		[TagDetails(Tag = 41842, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? UnderlyingOptionExerciseDate {get; set;}
		
		[TagDetails(Tag = 41843, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingOptionExerciseDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingOptionExerciseDate is not null) writer.WriteLocalDateOnly(41842, UnderlyingOptionExerciseDate.Value);
			if (UnderlyingOptionExerciseDateType is not null) writer.WriteWholeNumber(41843, UnderlyingOptionExerciseDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingOptionExerciseDate = view.GetDateOnly(41842);
			UnderlyingOptionExerciseDateType = view.GetInt32(41843);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingOptionExerciseDate":
					value = UnderlyingOptionExerciseDate;
					break;
				case "UnderlyingOptionExerciseDateType":
					value = UnderlyingOptionExerciseDateType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
