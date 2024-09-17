using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OptionExerciseExpirationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41140, Offset = 0, Required = false)]
		public NoOptionExerciseExpirationDateBusinessCenters[]? NoOptionExerciseExpirationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOptionExerciseExpirationDateBusinessCenters is not null && NoOptionExerciseExpirationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41140, NoOptionExerciseExpirationDateBusinessCenters.Length);
				for (int i = 0; i < NoOptionExerciseExpirationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoOptionExerciseExpirationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOptionExerciseExpirationDateBusinessCenters") is IMessageView viewNoOptionExerciseExpirationDateBusinessCenters)
			{
				var count = viewNoOptionExerciseExpirationDateBusinessCenters.GroupCount();
				NoOptionExerciseExpirationDateBusinessCenters = new NoOptionExerciseExpirationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoOptionExerciseExpirationDateBusinessCenters[i] = new();
					((IFixParser)NoOptionExerciseExpirationDateBusinessCenters[i]).Parse(viewNoOptionExerciseExpirationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOptionExerciseExpirationDateBusinessCenters":
					value = NoOptionExerciseExpirationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
