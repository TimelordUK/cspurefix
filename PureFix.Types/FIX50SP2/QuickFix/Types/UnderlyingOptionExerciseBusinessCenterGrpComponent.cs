using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingOptionExerciseBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41820, Offset = 0, Required = false)]
		public NoUnderlyingOptionExerciseBusinessCenters[]? NoUnderlyingOptionExerciseBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingOptionExerciseBusinessCenters is not null && NoUnderlyingOptionExerciseBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41820, NoUnderlyingOptionExerciseBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingOptionExerciseBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingOptionExerciseBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingOptionExerciseBusinessCenters") is IMessageView viewNoUnderlyingOptionExerciseBusinessCenters)
			{
				var count = viewNoUnderlyingOptionExerciseBusinessCenters.GroupCount();
				NoUnderlyingOptionExerciseBusinessCenters = new NoUnderlyingOptionExerciseBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingOptionExerciseBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingOptionExerciseBusinessCenters[i]).Parse(viewNoUnderlyingOptionExerciseBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingOptionExerciseBusinessCenters":
					value = NoUnderlyingOptionExerciseBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingOptionExerciseBusinessCenters = null;
		}
	}
}
