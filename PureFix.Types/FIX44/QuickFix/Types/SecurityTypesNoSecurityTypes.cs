using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecurityTypesNoSecurityTypes : IFixGroup
	{
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 0, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 762, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecuritySubType {get; set;}
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 2, Required = false)]
		public int? Product {get; set;}
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 3, Required = false)]
		public string? CFICode {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (SecuritySubType is not null) writer.WriteString(762, SecuritySubType);
			if (Product is not null) writer.WriteWholeNumber(460, Product.Value);
			if (CFICode is not null) writer.WriteString(461, CFICode);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			SecurityType = view.GetString(167);
			SecuritySubType = view.GetString(762);
			Product = view.GetInt32(460);
			CFICode = view.GetString(461);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "SecurityType":
					value = SecurityType;
					break;
				case "SecuritySubType":
					value = SecuritySubType;
					break;
				case "Product":
					value = Product;
					break;
				case "CFICode":
					value = CFICode;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			SecurityType = null;
			SecuritySubType = null;
			Product = null;
			CFICode = null;
		}
	}
}
