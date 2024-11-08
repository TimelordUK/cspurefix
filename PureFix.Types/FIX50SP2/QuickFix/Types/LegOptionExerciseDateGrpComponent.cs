using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegOptionExerciseDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41512, Offset = 0, Required = false)]
		public IOINoLegOptionExerciseDates[]? NoLegOptionExerciseDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegOptionExerciseDates is not null && NoLegOptionExerciseDates.Length != 0)
			{
				writer.WriteWholeNumber(41512, NoLegOptionExerciseDates.Length);
				for (int i = 0; i < NoLegOptionExerciseDates.Length; i++)
				{
					((IFixEncoder)NoLegOptionExerciseDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegOptionExerciseDates") is IMessageView viewNoLegOptionExerciseDates)
			{
				var count = viewNoLegOptionExerciseDates.GroupCount();
				NoLegOptionExerciseDates = new IOINoLegOptionExerciseDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegOptionExerciseDates[i] = new();
					((IFixParser)NoLegOptionExerciseDates[i]).Parse(viewNoLegOptionExerciseDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegOptionExerciseDates":
					value = NoLegOptionExerciseDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegOptionExerciseDates = null;
		}
	}
}
