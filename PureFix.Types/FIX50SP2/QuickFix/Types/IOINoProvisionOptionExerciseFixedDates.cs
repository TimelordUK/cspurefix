using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoProvisionOptionExerciseFixedDates : IFixGroup
	{
		[TagDetails(Tag = 40143, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? ProvisionOptionExerciseFixedDate {get; set;}
		
		[TagDetails(Tag = 40144, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ProvisionOptionExerciseFixedDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ProvisionOptionExerciseFixedDate is not null) writer.WriteLocalDateOnly(40143, ProvisionOptionExerciseFixedDate.Value);
			if (ProvisionOptionExerciseFixedDateType is not null) writer.WriteWholeNumber(40144, ProvisionOptionExerciseFixedDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ProvisionOptionExerciseFixedDate = view.GetDateOnly(40143);
			ProvisionOptionExerciseFixedDateType = view.GetInt32(40144);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ProvisionOptionExerciseFixedDate":
					value = ProvisionOptionExerciseFixedDate;
					break;
				case "ProvisionOptionExerciseFixedDateType":
					value = ProvisionOptionExerciseFixedDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ProvisionOptionExerciseFixedDate = null;
			ProvisionOptionExerciseFixedDateType = null;
		}
	}
}
