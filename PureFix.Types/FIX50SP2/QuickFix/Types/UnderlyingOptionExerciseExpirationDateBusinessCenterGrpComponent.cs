using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingOptionExerciseExpirationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41844, Offset = 0, Required = false)]
		public IOINoUnderlyingOptionExerciseExpirationDateBusinessCenters[]? NoUnderlyingOptionExerciseExpirationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingOptionExerciseExpirationDateBusinessCenters is not null && NoUnderlyingOptionExerciseExpirationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41844, NoUnderlyingOptionExerciseExpirationDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingOptionExerciseExpirationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingOptionExerciseExpirationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingOptionExerciseExpirationDateBusinessCenters") is IMessageView viewNoUnderlyingOptionExerciseExpirationDateBusinessCenters)
			{
				var count = viewNoUnderlyingOptionExerciseExpirationDateBusinessCenters.GroupCount();
				NoUnderlyingOptionExerciseExpirationDateBusinessCenters = new IOINoUnderlyingOptionExerciseExpirationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingOptionExerciseExpirationDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingOptionExerciseExpirationDateBusinessCenters[i]).Parse(viewNoUnderlyingOptionExerciseExpirationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingOptionExerciseExpirationDateBusinessCenters":
					value = NoUnderlyingOptionExerciseExpirationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingOptionExerciseExpirationDateBusinessCenters = null;
		}
	}
}
