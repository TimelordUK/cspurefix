using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoReturnRateInformationSources : IFixGroup
	{
		[TagDetails(Tag = 42762, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ReturnRateInformationSource {get; set;}
		
		[TagDetails(Tag = 42763, Type = TagType.String, Offset = 1, Required = false)]
		public string? ReturnRateReferencePage {get; set;}
		
		[TagDetails(Tag = 42764, Type = TagType.String, Offset = 2, Required = false)]
		public string? ReturnRateReferencePageHeading {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ReturnRateInformationSource is not null) writer.WriteWholeNumber(42762, ReturnRateInformationSource.Value);
			if (ReturnRateReferencePage is not null) writer.WriteString(42763, ReturnRateReferencePage);
			if (ReturnRateReferencePageHeading is not null) writer.WriteString(42764, ReturnRateReferencePageHeading);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ReturnRateInformationSource = view.GetInt32(42762);
			ReturnRateReferencePage = view.GetString(42763);
			ReturnRateReferencePageHeading = view.GetString(42764);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ReturnRateInformationSource":
					value = ReturnRateInformationSource;
					break;
				case "ReturnRateReferencePage":
					value = ReturnRateReferencePage;
					break;
				case "ReturnRateReferencePageHeading":
					value = ReturnRateReferencePageHeading;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ReturnRateInformationSource = null;
			ReturnRateReferencePage = null;
			ReturnRateReferencePageHeading = null;
		}
	}
}
