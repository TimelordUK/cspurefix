using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoComplexEventCreditEventQualifiers : IFixGroup
	{
		[TagDetails(Tag = 41006, Type = TagType.String, Offset = 0, Required = false)]
		public string? ComplexEventCreditEventQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ComplexEventCreditEventQualifier is not null) writer.WriteString(41006, ComplexEventCreditEventQualifier);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ComplexEventCreditEventQualifier = view.GetString(41006);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ComplexEventCreditEventQualifier":
					value = ComplexEventCreditEventQualifier;
					break;
				default: return false;
			}
			return true;
		}
	}
}
