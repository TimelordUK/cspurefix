using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityDefinitionNoSecurityClassifications : IFixGroup
	{
		[TagDetails(Tag = 1583, Type = TagType.Int, Offset = 0, Required = false)]
		public int? SecurityClassificationReason {get; set;}
		
		[TagDetails(Tag = 1584, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecurityClassificationValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SecurityClassificationReason is not null) writer.WriteWholeNumber(1583, SecurityClassificationReason.Value);
			if (SecurityClassificationValue is not null) writer.WriteString(1584, SecurityClassificationValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			SecurityClassificationReason = view.GetInt32(1583);
			SecurityClassificationValue = view.GetString(1584);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "SecurityClassificationReason":
					value = SecurityClassificationReason;
					break;
				case "SecurityClassificationValue":
					value = SecurityClassificationValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			SecurityClassificationReason = null;
			SecurityClassificationValue = null;
		}
	}
}
