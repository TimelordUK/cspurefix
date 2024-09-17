using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoEntitlementAttrib : IFixGroup
	{
		[TagDetails(Tag = 1778, Type = TagType.Int, Offset = 0, Required = false)]
		public int? EntitlementAttribType {get; set;}
		
		[TagDetails(Tag = 1779, Type = TagType.Int, Offset = 1, Required = false)]
		public int? EntitlementAttribDatatype {get; set;}
		
		[TagDetails(Tag = 1780, Type = TagType.String, Offset = 2, Required = false)]
		public string? EntitlementAttribValue {get; set;}
		
		[TagDetails(Tag = 1781, Type = TagType.String, Offset = 3, Required = false)]
		public string? EntitlementAttribCurrency {get; set;}
		
		[TagDetails(Tag = 2940, Type = TagType.String, Offset = 4, Required = false)]
		public string? EntitlementAttribCurrencyCodeSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (EntitlementAttribType is not null) writer.WriteWholeNumber(1778, EntitlementAttribType.Value);
			if (EntitlementAttribDatatype is not null) writer.WriteWholeNumber(1779, EntitlementAttribDatatype.Value);
			if (EntitlementAttribValue is not null) writer.WriteString(1780, EntitlementAttribValue);
			if (EntitlementAttribCurrency is not null) writer.WriteString(1781, EntitlementAttribCurrency);
			if (EntitlementAttribCurrencyCodeSource is not null) writer.WriteString(2940, EntitlementAttribCurrencyCodeSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			EntitlementAttribType = view.GetInt32(1778);
			EntitlementAttribDatatype = view.GetInt32(1779);
			EntitlementAttribValue = view.GetString(1780);
			EntitlementAttribCurrency = view.GetString(1781);
			EntitlementAttribCurrencyCodeSource = view.GetString(2940);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "EntitlementAttribType":
					value = EntitlementAttribType;
					break;
				case "EntitlementAttribDatatype":
					value = EntitlementAttribDatatype;
					break;
				case "EntitlementAttribValue":
					value = EntitlementAttribValue;
					break;
				case "EntitlementAttribCurrency":
					value = EntitlementAttribCurrency;
					break;
				case "EntitlementAttribCurrencyCodeSource":
					value = EntitlementAttribCurrencyCodeSource;
					break;
				default: return false;
			}
			return true;
		}
	}
}
