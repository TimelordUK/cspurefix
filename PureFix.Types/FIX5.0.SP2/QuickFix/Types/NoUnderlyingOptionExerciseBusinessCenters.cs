using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingOptionExerciseBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41821, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingOptionExerciseBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingOptionExerciseBusinessCenter is not null) writer.WriteString(41821, UnderlyingOptionExerciseBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingOptionExerciseBusinessCenter = view.GetString(41821);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingOptionExerciseBusinessCenter":
					value = UnderlyingOptionExerciseBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
