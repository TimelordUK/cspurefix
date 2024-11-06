using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionOptionExerciseBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40936, Offset = 0, Required = false)]
		public IOINoLegProvisionOptionExerciseBusinessCenters[]? NoLegProvisionOptionExerciseBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionOptionExerciseBusinessCenters is not null && NoLegProvisionOptionExerciseBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40936, NoLegProvisionOptionExerciseBusinessCenters.Length);
				for (int i = 0; i < NoLegProvisionOptionExerciseBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegProvisionOptionExerciseBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionOptionExerciseBusinessCenters") is IMessageView viewNoLegProvisionOptionExerciseBusinessCenters)
			{
				var count = viewNoLegProvisionOptionExerciseBusinessCenters.GroupCount();
				NoLegProvisionOptionExerciseBusinessCenters = new IOINoLegProvisionOptionExerciseBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionOptionExerciseBusinessCenters[i] = new();
					((IFixParser)NoLegProvisionOptionExerciseBusinessCenters[i]).Parse(viewNoLegProvisionOptionExerciseBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionOptionExerciseBusinessCenters":
					value = NoLegProvisionOptionExerciseBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProvisionOptionExerciseBusinessCenters = null;
		}
	}
}
