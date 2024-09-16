using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPriceQualifiers : IFixGroup
	{
		[TagDetails(Tag = 2710, Type = TagType.Int, Offset = 0, Required = false)]
		public int? PriceQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PriceQualifier is not null) writer.WriteWholeNumber(2710, PriceQualifier.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PriceQualifier = view.GetInt32(2710);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PriceQualifier":
					value = PriceQualifier;
					break;
				default: return false;
			}
			return true;
		}
	}
}
