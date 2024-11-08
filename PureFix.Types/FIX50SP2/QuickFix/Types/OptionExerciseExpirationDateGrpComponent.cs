using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OptionExerciseExpirationDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41152, Offset = 0, Required = false)]
		public IOINoOptionExerciseExpirationDates[]? NoOptionExerciseExpirationDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOptionExerciseExpirationDates is not null && NoOptionExerciseExpirationDates.Length != 0)
			{
				writer.WriteWholeNumber(41152, NoOptionExerciseExpirationDates.Length);
				for (int i = 0; i < NoOptionExerciseExpirationDates.Length; i++)
				{
					((IFixEncoder)NoOptionExerciseExpirationDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOptionExerciseExpirationDates") is IMessageView viewNoOptionExerciseExpirationDates)
			{
				var count = viewNoOptionExerciseExpirationDates.GroupCount();
				NoOptionExerciseExpirationDates = new IOINoOptionExerciseExpirationDates[count];
				for (int i = 0; i < count; i++)
				{
					NoOptionExerciseExpirationDates[i] = new();
					((IFixParser)NoOptionExerciseExpirationDates[i]).Parse(viewNoOptionExerciseExpirationDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOptionExerciseExpirationDates":
					value = NoOptionExerciseExpirationDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoOptionExerciseExpirationDates = null;
		}
	}
}
