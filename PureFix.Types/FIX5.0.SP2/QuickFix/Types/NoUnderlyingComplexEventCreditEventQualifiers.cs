using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingComplexEventCreditEventQualifiers : IFixGroup
	{
		[TagDetails(Tag = 41725, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingComplexEventCreditEventQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingComplexEventCreditEventQualifier is not null) writer.WriteString(41725, UnderlyingComplexEventCreditEventQualifier);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingComplexEventCreditEventQualifier = view.GetString(41725);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingComplexEventCreditEventQualifier":
					value = UnderlyingComplexEventCreditEventQualifier;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingComplexEventCreditEventQualifier = null;
		}
	}
}
