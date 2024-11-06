using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OptionExerciseBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41116, Offset = 0, Required = false)]
		public IOINoOptionExerciseBusinessCenters[]? NoOptionExerciseBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOptionExerciseBusinessCenters is not null && NoOptionExerciseBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41116, NoOptionExerciseBusinessCenters.Length);
				for (int i = 0; i < NoOptionExerciseBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoOptionExerciseBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOptionExerciseBusinessCenters") is IMessageView viewNoOptionExerciseBusinessCenters)
			{
				var count = viewNoOptionExerciseBusinessCenters.GroupCount();
				NoOptionExerciseBusinessCenters = new IOINoOptionExerciseBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoOptionExerciseBusinessCenters[i] = new();
					((IFixParser)NoOptionExerciseBusinessCenters[i]).Parse(viewNoOptionExerciseBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOptionExerciseBusinessCenters":
					value = NoOptionExerciseBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoOptionExerciseBusinessCenters = null;
		}
	}
}
