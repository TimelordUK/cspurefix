using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AdvertisementNoLegFinancingTermSupplements : IFixGroup
	{
		[TagDetails(Tag = 42201, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegFinancingTermSupplementDesc {get; set;}
		
		[TagDetails(Tag = 42202, Type = TagType.LocalDate, Offset = 1, Required = false)]
		public DateOnly? LegFinancingTermSupplementDate {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegFinancingTermSupplementDesc is not null) writer.WriteString(42201, LegFinancingTermSupplementDesc);
			if (LegFinancingTermSupplementDate is not null) writer.WriteLocalDateOnly(42202, LegFinancingTermSupplementDate.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegFinancingTermSupplementDesc = view.GetString(42201);
			LegFinancingTermSupplementDate = view.GetDateOnly(42202);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegFinancingTermSupplementDesc":
					value = LegFinancingTermSupplementDesc;
					break;
				case "LegFinancingTermSupplementDate":
					value = LegFinancingTermSupplementDate;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegFinancingTermSupplementDesc = null;
			LegFinancingTermSupplementDate = null;
		}
	}
}
