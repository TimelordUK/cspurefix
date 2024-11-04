using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegOptionExerciseBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41492, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegOptionExerciseBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegOptionExerciseBusinessCenter is not null) writer.WriteString(41492, LegOptionExerciseBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegOptionExerciseBusinessCenter = view.GetString(41492);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegOptionExerciseBusinessCenter":
					value = LegOptionExerciseBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegOptionExerciseBusinessCenter = null;
		}
	}
}
