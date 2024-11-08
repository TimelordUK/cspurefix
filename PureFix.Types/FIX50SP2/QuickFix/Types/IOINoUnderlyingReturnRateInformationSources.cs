using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingReturnRateInformationSources : IFixGroup
	{
		[TagDetails(Tag = 43061, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingReturnRateInformationSource {get; set;}
		
		[TagDetails(Tag = 43062, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingReturnRateReferencePage {get; set;}
		
		[TagDetails(Tag = 43063, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingReturnRateReferencePageHeading {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingReturnRateInformationSource is not null) writer.WriteWholeNumber(43061, UnderlyingReturnRateInformationSource.Value);
			if (UnderlyingReturnRateReferencePage is not null) writer.WriteString(43062, UnderlyingReturnRateReferencePage);
			if (UnderlyingReturnRateReferencePageHeading is not null) writer.WriteString(43063, UnderlyingReturnRateReferencePageHeading);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingReturnRateInformationSource = view.GetInt32(43061);
			UnderlyingReturnRateReferencePage = view.GetString(43062);
			UnderlyingReturnRateReferencePageHeading = view.GetString(43063);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingReturnRateInformationSource":
					value = UnderlyingReturnRateInformationSource;
					break;
				case "UnderlyingReturnRateReferencePage":
					value = UnderlyingReturnRateReferencePage;
					break;
				case "UnderlyingReturnRateReferencePageHeading":
					value = UnderlyingReturnRateReferencePageHeading;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingReturnRateInformationSource = null;
			UnderlyingReturnRateReferencePage = null;
			UnderlyingReturnRateReferencePageHeading = null;
		}
	}
}
