using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegOptionExerciseBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41491, Offset = 0, Required = false)]
		public NoLegOptionExerciseBusinessCenters[]? NoLegOptionExerciseBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegOptionExerciseBusinessCenters is not null && NoLegOptionExerciseBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41491, NoLegOptionExerciseBusinessCenters.Length);
				for (int i = 0; i < NoLegOptionExerciseBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegOptionExerciseBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegOptionExerciseBusinessCenters") is IMessageView viewNoLegOptionExerciseBusinessCenters)
			{
				var count = viewNoLegOptionExerciseBusinessCenters.GroupCount();
				NoLegOptionExerciseBusinessCenters = new NoLegOptionExerciseBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegOptionExerciseBusinessCenters[i] = new();
					((IFixParser)NoLegOptionExerciseBusinessCenters[i]).Parse(viewNoLegOptionExerciseBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegOptionExerciseBusinessCenters":
					value = NoLegOptionExerciseBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
