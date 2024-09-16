using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegOptionExerciseExpirationDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41516, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegOptionExerciseExpirationDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegOptionExerciseExpirationDateBusinessCenter is not null) writer.WriteString(41516, LegOptionExerciseExpirationDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegOptionExerciseExpirationDateBusinessCenter = view.GetString(41516);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegOptionExerciseExpirationDateBusinessCenter":
					value = LegOptionExerciseExpirationDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
