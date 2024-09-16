using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegOptionExerciseDates : IFixGroup
	{
		[TagDetails(Tag = 41513, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegOptionExerciseDate {get; set;}
		
		[TagDetails(Tag = 41514, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegOptionExerciseDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegOptionExerciseDate is not null) writer.WriteLocalDateOnly(41513, LegOptionExerciseDate.Value);
			if (LegOptionExerciseDateType is not null) writer.WriteWholeNumber(41514, LegOptionExerciseDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegOptionExerciseDate = view.GetDateOnly(41513);
			LegOptionExerciseDateType = view.GetInt32(41514);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegOptionExerciseDate":
					value = LegOptionExerciseDate;
					break;
				case "LegOptionExerciseDateType":
					value = LegOptionExerciseDateType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
