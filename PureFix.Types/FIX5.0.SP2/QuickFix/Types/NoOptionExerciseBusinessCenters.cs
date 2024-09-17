using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoOptionExerciseBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41117, Type = TagType.String, Offset = 0, Required = false)]
		public string? OptionExerciseBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OptionExerciseBusinessCenter is not null) writer.WriteString(41117, OptionExerciseBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OptionExerciseBusinessCenter = view.GetString(41117);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "OptionExerciseBusinessCenter":
					value = OptionExerciseBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
