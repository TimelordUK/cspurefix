using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegProvisionOptionExerciseBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40477, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProvisionOptionExerciseBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionOptionExerciseBusinessCenter is not null) writer.WriteString(40477, LegProvisionOptionExerciseBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionOptionExerciseBusinessCenter = view.GetString(40477);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionOptionExerciseBusinessCenter":
					value = LegProvisionOptionExerciseBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegProvisionOptionExerciseBusinessCenter = null;
		}
	}
}
