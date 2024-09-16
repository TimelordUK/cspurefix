using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class SecurityTypesNoSecurityTypes : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 0, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 1, Required = false)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 2, Required = false)]
		public string? CFICode { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (Product is not null) writer.WriteWholeNumber(460, Product.Value);
			if (CFICode is not null) writer.WriteString(461, CFICode);
		}
	}
}
