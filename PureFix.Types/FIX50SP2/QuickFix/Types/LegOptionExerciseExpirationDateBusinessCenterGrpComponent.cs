using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegOptionExerciseExpirationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41515, Offset = 0, Required = false)]
		public NoLegOptionExerciseExpirationDateBusinessCenters[]? NoLegOptionExerciseExpirationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegOptionExerciseExpirationDateBusinessCenters is not null && NoLegOptionExerciseExpirationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41515, NoLegOptionExerciseExpirationDateBusinessCenters.Length);
				for (int i = 0; i < NoLegOptionExerciseExpirationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegOptionExerciseExpirationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegOptionExerciseExpirationDateBusinessCenters") is IMessageView viewNoLegOptionExerciseExpirationDateBusinessCenters)
			{
				var count = viewNoLegOptionExerciseExpirationDateBusinessCenters.GroupCount();
				NoLegOptionExerciseExpirationDateBusinessCenters = new NoLegOptionExerciseExpirationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegOptionExerciseExpirationDateBusinessCenters[i] = new();
					((IFixParser)NoLegOptionExerciseExpirationDateBusinessCenters[i]).Parse(viewNoLegOptionExerciseExpirationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegOptionExerciseExpirationDateBusinessCenters":
					value = NoLegOptionExerciseExpirationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegOptionExerciseExpirationDateBusinessCenters = null;
		}
	}
}
