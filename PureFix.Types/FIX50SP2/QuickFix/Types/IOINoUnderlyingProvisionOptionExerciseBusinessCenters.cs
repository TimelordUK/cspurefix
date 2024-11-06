using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingProvisionOptionExerciseBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42185, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProvisionOptionExerciseBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionOptionExerciseBusinessCenter is not null) writer.WriteString(42185, UnderlyingProvisionOptionExerciseBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionOptionExerciseBusinessCenter = view.GetString(42185);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionOptionExerciseBusinessCenter":
					value = UnderlyingProvisionOptionExerciseBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingProvisionOptionExerciseBusinessCenter = null;
		}
	}
}
