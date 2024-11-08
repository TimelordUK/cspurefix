using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegOptionExerciseExpirationDates : IFixGroup
	{
		[TagDetails(Tag = 41528, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegOptionExerciseExpirationDate {get; set;}
		
		[TagDetails(Tag = 41529, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegOptionExerciseExpirationDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegOptionExerciseExpirationDate is not null) writer.WriteLocalDateOnly(41528, LegOptionExerciseExpirationDate.Value);
			if (LegOptionExerciseExpirationDateType is not null) writer.WriteWholeNumber(41529, LegOptionExerciseExpirationDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegOptionExerciseExpirationDate = view.GetDateOnly(41528);
			LegOptionExerciseExpirationDateType = view.GetInt32(41529);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegOptionExerciseExpirationDate":
					value = LegOptionExerciseExpirationDate;
					break;
				case "LegOptionExerciseExpirationDateType":
					value = LegOptionExerciseExpirationDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegOptionExerciseExpirationDate = null;
			LegOptionExerciseExpirationDateType = null;
		}
	}
}
