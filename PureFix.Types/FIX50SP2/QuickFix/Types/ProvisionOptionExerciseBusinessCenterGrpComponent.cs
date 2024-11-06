using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionOptionExerciseBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40954, Offset = 0, Required = false)]
		public IOINoProvisionOptionExerciseBusinessCenters[]? NoProvisionOptionExerciseBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionOptionExerciseBusinessCenters is not null && NoProvisionOptionExerciseBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40954, NoProvisionOptionExerciseBusinessCenters.Length);
				for (int i = 0; i < NoProvisionOptionExerciseBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoProvisionOptionExerciseBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionOptionExerciseBusinessCenters") is IMessageView viewNoProvisionOptionExerciseBusinessCenters)
			{
				var count = viewNoProvisionOptionExerciseBusinessCenters.GroupCount();
				NoProvisionOptionExerciseBusinessCenters = new IOINoProvisionOptionExerciseBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionOptionExerciseBusinessCenters[i] = new();
					((IFixParser)NoProvisionOptionExerciseBusinessCenters[i]).Parse(viewNoProvisionOptionExerciseBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionOptionExerciseBusinessCenters":
					value = NoProvisionOptionExerciseBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProvisionOptionExerciseBusinessCenters = null;
		}
	}
}
