using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegProtectionTermObligations : IFixGroup
	{
		[TagDetails(Tag = 41636, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProtectionTermObligationType {get; set;}
		
		[TagDetails(Tag = 41637, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegProtectionTermObligationValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProtectionTermObligationType is not null) writer.WriteString(41636, LegProtectionTermObligationType);
			if (LegProtectionTermObligationValue is not null) writer.WriteString(41637, LegProtectionTermObligationValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProtectionTermObligationType = view.GetString(41636);
			LegProtectionTermObligationValue = view.GetString(41637);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProtectionTermObligationType":
					value = LegProtectionTermObligationType;
					break;
				case "LegProtectionTermObligationValue":
					value = LegProtectionTermObligationValue;
					break;
				default: return false;
			}
			return true;
		}
	}
}
