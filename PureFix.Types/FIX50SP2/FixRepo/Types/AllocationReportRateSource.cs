using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class AllocationReportRateSource : IFixGroup
	{
		[TagDetails(Tag = 1446, Type = TagType.Int, Offset = 0, Required = false)]
		public int? RateSource {get; set;}
		
		[TagDetails(Tag = 1447, Type = TagType.Int, Offset = 1, Required = false)]
		public int? RateSourceType {get; set;}
		
		[TagDetails(Tag = 1448, Type = TagType.String, Offset = 2, Required = false)]
		public string? ReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RateSource is not null) writer.WriteWholeNumber(1446, RateSource.Value);
			if (RateSourceType is not null) writer.WriteWholeNumber(1447, RateSourceType.Value);
			if (ReferencePage is not null) writer.WriteString(1448, ReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RateSource = view.GetInt32(1446);
			RateSourceType = view.GetInt32(1447);
			ReferencePage = view.GetString(1448);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RateSource":
					value = RateSource;
					break;
				case "RateSourceType":
					value = RateSourceType;
					break;
				case "ReferencePage":
					value = ReferencePage;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RateSource = null;
			RateSourceType = null;
			ReferencePage = null;
		}
	}
}
