using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingOptionExerciseExpirationDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41856, Offset = 0, Required = false)]
		public NoUnderlyingOptionExerciseExpirationDates[]? NoUnderlyingOptionExerciseExpirationDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingOptionExerciseExpirationDates is not null && NoUnderlyingOptionExerciseExpirationDates.Length != 0)
			{
				writer.WriteWholeNumber(41856, NoUnderlyingOptionExerciseExpirationDates.Length);
				for (int i = 0; i < NoUnderlyingOptionExerciseExpirationDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingOptionExerciseExpirationDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingOptionExerciseExpirationDates") is IMessageView viewNoUnderlyingOptionExerciseExpirationDates)
			{
				var count = viewNoUnderlyingOptionExerciseExpirationDates.GroupCount();
				NoUnderlyingOptionExerciseExpirationDates = new NoUnderlyingOptionExerciseExpirationDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingOptionExerciseExpirationDates[i] = new();
					((IFixParser)NoUnderlyingOptionExerciseExpirationDates[i]).Parse(viewNoUnderlyingOptionExerciseExpirationDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingOptionExerciseExpirationDates":
					value = NoUnderlyingOptionExerciseExpirationDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
