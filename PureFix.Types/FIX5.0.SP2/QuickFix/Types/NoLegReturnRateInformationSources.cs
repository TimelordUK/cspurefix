using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegReturnRateInformationSources : IFixGroup
	{
		[TagDetails(Tag = 42561, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegReturnRateInformationSource {get; set;}
		
		[TagDetails(Tag = 42562, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegReturnRateReferencePage {get; set;}
		
		[TagDetails(Tag = 42563, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegReturnRateReferencePageHeading {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegReturnRateInformationSource is not null) writer.WriteWholeNumber(42561, LegReturnRateInformationSource.Value);
			if (LegReturnRateReferencePage is not null) writer.WriteString(42562, LegReturnRateReferencePage);
			if (LegReturnRateReferencePageHeading is not null) writer.WriteString(42563, LegReturnRateReferencePageHeading);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegReturnRateInformationSource = view.GetInt32(42561);
			LegReturnRateReferencePage = view.GetString(42562);
			LegReturnRateReferencePageHeading = view.GetString(42563);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegReturnRateInformationSource":
					value = LegReturnRateInformationSource;
					break;
				case "LegReturnRateReferencePage":
					value = LegReturnRateReferencePage;
					break;
				case "LegReturnRateReferencePageHeading":
					value = LegReturnRateReferencePageHeading;
					break;
				default: return false;
			}
			return true;
		}
	}
}
