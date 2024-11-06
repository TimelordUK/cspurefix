using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AdvertisementNoLegContractualDefinitions : IFixGroup
	{
		[TagDetails(Tag = 42199, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegContractualDefinition {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegContractualDefinition is not null) writer.WriteString(42199, LegContractualDefinition);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegContractualDefinition = view.GetString(42199);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegContractualDefinition":
					value = LegContractualDefinition;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegContractualDefinition = null;
		}
	}
}
