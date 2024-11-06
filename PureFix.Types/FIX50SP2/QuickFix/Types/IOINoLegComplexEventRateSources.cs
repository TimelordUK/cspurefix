using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegComplexEventRateSources : IFixGroup
	{
		[TagDetails(Tag = 41383, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegComplexEventRateSource {get; set;}
		
		[TagDetails(Tag = 41384, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegComplexEventRateSourceType {get; set;}
		
		[TagDetails(Tag = 41385, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegComplexEventReferencePage {get; set;}
		
		[TagDetails(Tag = 41386, Type = TagType.String, Offset = 3, Required = false)]
		public string? LegComplexEvenReferencePageHeading {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegComplexEventRateSource is not null) writer.WriteWholeNumber(41383, LegComplexEventRateSource.Value);
			if (LegComplexEventRateSourceType is not null) writer.WriteWholeNumber(41384, LegComplexEventRateSourceType.Value);
			if (LegComplexEventReferencePage is not null) writer.WriteString(41385, LegComplexEventReferencePage);
			if (LegComplexEvenReferencePageHeading is not null) writer.WriteString(41386, LegComplexEvenReferencePageHeading);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegComplexEventRateSource = view.GetInt32(41383);
			LegComplexEventRateSourceType = view.GetInt32(41384);
			LegComplexEventReferencePage = view.GetString(41385);
			LegComplexEvenReferencePageHeading = view.GetString(41386);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegComplexEventRateSource":
					value = LegComplexEventRateSource;
					break;
				case "LegComplexEventRateSourceType":
					value = LegComplexEventRateSourceType;
					break;
				case "LegComplexEventReferencePage":
					value = LegComplexEventReferencePage;
					break;
				case "LegComplexEvenReferencePageHeading":
					value = LegComplexEvenReferencePageHeading;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegComplexEventRateSource = null;
			LegComplexEventRateSourceType = null;
			LegComplexEventReferencePage = null;
			LegComplexEvenReferencePageHeading = null;
		}
	}
}
