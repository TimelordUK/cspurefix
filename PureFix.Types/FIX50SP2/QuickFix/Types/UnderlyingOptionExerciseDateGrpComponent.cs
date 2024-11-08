using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingOptionExerciseDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41841, Offset = 0, Required = false)]
		public IOINoUnderlyingOptionExerciseDates[]? NoUnderlyingOptionExerciseDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingOptionExerciseDates is not null && NoUnderlyingOptionExerciseDates.Length != 0)
			{
				writer.WriteWholeNumber(41841, NoUnderlyingOptionExerciseDates.Length);
				for (int i = 0; i < NoUnderlyingOptionExerciseDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingOptionExerciseDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingOptionExerciseDates") is IMessageView viewNoUnderlyingOptionExerciseDates)
			{
				var count = viewNoUnderlyingOptionExerciseDates.GroupCount();
				NoUnderlyingOptionExerciseDates = new IOINoUnderlyingOptionExerciseDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingOptionExerciseDates[i] = new();
					((IFixParser)NoUnderlyingOptionExerciseDates[i]).Parse(viewNoUnderlyingOptionExerciseDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingOptionExerciseDates":
					value = NoUnderlyingOptionExerciseDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingOptionExerciseDates = null;
		}
	}
}
