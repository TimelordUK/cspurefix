using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoComplexEventRateSources : IFixGroup
	{
		[TagDetails(Tag = 41014, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ComplexEventRateSource {get; set;}
		
		[TagDetails(Tag = 41015, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ComplexEventRateSourceType {get; set;}
		
		[TagDetails(Tag = 41016, Type = TagType.String, Offset = 2, Required = false)]
		public string? ComplexEventReferencePage {get; set;}
		
		[TagDetails(Tag = 41017, Type = TagType.String, Offset = 3, Required = false)]
		public string? ComplexEventReferencePageHeading {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ComplexEventRateSource is not null) writer.WriteWholeNumber(41014, ComplexEventRateSource.Value);
			if (ComplexEventRateSourceType is not null) writer.WriteWholeNumber(41015, ComplexEventRateSourceType.Value);
			if (ComplexEventReferencePage is not null) writer.WriteString(41016, ComplexEventReferencePage);
			if (ComplexEventReferencePageHeading is not null) writer.WriteString(41017, ComplexEventReferencePageHeading);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ComplexEventRateSource = view.GetInt32(41014);
			ComplexEventRateSourceType = view.GetInt32(41015);
			ComplexEventReferencePage = view.GetString(41016);
			ComplexEventReferencePageHeading = view.GetString(41017);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ComplexEventRateSource":
					value = ComplexEventRateSource;
					break;
				case "ComplexEventRateSourceType":
					value = ComplexEventRateSourceType;
					break;
				case "ComplexEventReferencePage":
					value = ComplexEventReferencePage;
					break;
				case "ComplexEventReferencePageHeading":
					value = ComplexEventReferencePageHeading;
					break;
				default: return false;
			}
			return true;
		}
	}
}
