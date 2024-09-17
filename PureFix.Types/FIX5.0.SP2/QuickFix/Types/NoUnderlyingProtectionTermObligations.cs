using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingProtectionTermObligations : IFixGroup
	{
		[TagDetails(Tag = 42088, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProtectionTermObligationType {get; set;}
		
		[TagDetails(Tag = 42089, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingProtectionTermObligationValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProtectionTermObligationType is not null) writer.WriteString(42088, UnderlyingProtectionTermObligationType);
			if (UnderlyingProtectionTermObligationValue is not null) writer.WriteString(42089, UnderlyingProtectionTermObligationValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProtectionTermObligationType = view.GetString(42088);
			UnderlyingProtectionTermObligationValue = view.GetString(42089);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProtectionTermObligationType":
					value = UnderlyingProtectionTermObligationType;
					break;
				case "UnderlyingProtectionTermObligationValue":
					value = UnderlyingProtectionTermObligationValue;
					break;
				default: return false;
			}
			return true;
		}
	}
}
