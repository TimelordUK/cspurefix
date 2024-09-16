using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionOptionExerciseFixedDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42112, Offset = 0, Required = false)]
		public NoUnderlyingProvisionOptionExerciseFixedDates[]? NoUnderlyingProvisionOptionExerciseFixedDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionOptionExerciseFixedDates is not null && NoUnderlyingProvisionOptionExerciseFixedDates.Length != 0)
			{
				writer.WriteWholeNumber(42112, NoUnderlyingProvisionOptionExerciseFixedDates.Length);
				for (int i = 0; i < NoUnderlyingProvisionOptionExerciseFixedDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionOptionExerciseFixedDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionOptionExerciseFixedDates") is IMessageView viewNoUnderlyingProvisionOptionExerciseFixedDates)
			{
				var count = viewNoUnderlyingProvisionOptionExerciseFixedDates.GroupCount();
				NoUnderlyingProvisionOptionExerciseFixedDates = new NoUnderlyingProvisionOptionExerciseFixedDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionOptionExerciseFixedDates[i] = new();
					((IFixParser)NoUnderlyingProvisionOptionExerciseFixedDates[i]).Parse(viewNoUnderlyingProvisionOptionExerciseFixedDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionOptionExerciseFixedDates":
					value = NoUnderlyingProvisionOptionExerciseFixedDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
