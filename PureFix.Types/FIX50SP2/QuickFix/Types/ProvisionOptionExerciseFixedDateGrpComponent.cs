using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionOptionExerciseFixedDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40142, Offset = 0, Required = false)]
		public IOINoProvisionOptionExerciseFixedDates[]? NoProvisionOptionExerciseFixedDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionOptionExerciseFixedDates is not null && NoProvisionOptionExerciseFixedDates.Length != 0)
			{
				writer.WriteWholeNumber(40142, NoProvisionOptionExerciseFixedDates.Length);
				for (int i = 0; i < NoProvisionOptionExerciseFixedDates.Length; i++)
				{
					((IFixEncoder)NoProvisionOptionExerciseFixedDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionOptionExerciseFixedDates") is IMessageView viewNoProvisionOptionExerciseFixedDates)
			{
				var count = viewNoProvisionOptionExerciseFixedDates.GroupCount();
				NoProvisionOptionExerciseFixedDates = new IOINoProvisionOptionExerciseFixedDates[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionOptionExerciseFixedDates[i] = new();
					((IFixParser)NoProvisionOptionExerciseFixedDates[i]).Parse(viewNoProvisionOptionExerciseFixedDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionOptionExerciseFixedDates":
					value = NoProvisionOptionExerciseFixedDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProvisionOptionExerciseFixedDates = null;
		}
	}
}
