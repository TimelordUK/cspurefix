using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OptionExerciseDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41137, Offset = 0, Required = false)]
		public NoOptionExerciseDates[]? NoOptionExerciseDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOptionExerciseDates is not null && NoOptionExerciseDates.Length != 0)
			{
				writer.WriteWholeNumber(41137, NoOptionExerciseDates.Length);
				for (int i = 0; i < NoOptionExerciseDates.Length; i++)
				{
					((IFixEncoder)NoOptionExerciseDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOptionExerciseDates") is IMessageView viewNoOptionExerciseDates)
			{
				var count = viewNoOptionExerciseDates.GroupCount();
				NoOptionExerciseDates = new NoOptionExerciseDates[count];
				for (int i = 0; i < count; i++)
				{
					NoOptionExerciseDates[i] = new();
					((IFixParser)NoOptionExerciseDates[i]).Parse(viewNoOptionExerciseDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOptionExerciseDates":
					value = NoOptionExerciseDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
