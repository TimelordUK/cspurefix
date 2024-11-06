using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoProtectionTermObligations : IFixGroup
	{
		[TagDetails(Tag = 40202, Type = TagType.String, Offset = 0, Required = false)]
		public string? ProtectionTermObligationType {get; set;}
		
		[TagDetails(Tag = 40203, Type = TagType.String, Offset = 1, Required = false)]
		public string? ProtectionTermObligationValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ProtectionTermObligationType is not null) writer.WriteString(40202, ProtectionTermObligationType);
			if (ProtectionTermObligationValue is not null) writer.WriteString(40203, ProtectionTermObligationValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ProtectionTermObligationType = view.GetString(40202);
			ProtectionTermObligationValue = view.GetString(40203);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ProtectionTermObligationType":
					value = ProtectionTermObligationType;
					break;
				case "ProtectionTermObligationValue":
					value = ProtectionTermObligationValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ProtectionTermObligationType = null;
			ProtectionTermObligationValue = null;
		}
	}
}
