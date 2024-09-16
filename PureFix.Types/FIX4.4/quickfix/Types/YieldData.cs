using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class YieldData : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 235, Type = TagType.String, Offset = 0, Required = false)]
		public string? YieldType { get; set; }
		
		[TagDetails(Tag = 236, Type = TagType.Float, Offset = 1, Required = false)]
		public double? Yield { get; set; }
		
		[TagDetails(Tag = 701, Type = TagType.LocalDate, Offset = 2, Required = false)]
		public DateOnly? YieldCalcDate { get; set; }
		
		[TagDetails(Tag = 696, Type = TagType.LocalDate, Offset = 3, Required = false)]
		public DateOnly? YieldRedemptionDate { get; set; }
		
		[TagDetails(Tag = 697, Type = TagType.Float, Offset = 4, Required = false)]
		public double? YieldRedemptionPrice { get; set; }
		
		[TagDetails(Tag = 698, Type = TagType.Int, Offset = 5, Required = false)]
		public int? YieldRedemptionPriceType { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (YieldType is not null) writer.WriteString(235, YieldType);
			if (Yield is not null) writer.WriteNumber(236, Yield.Value);
			if (YieldCalcDate is not null) writer.WriteLocalDateOnly(701, YieldCalcDate.Value);
			if (YieldRedemptionDate is not null) writer.WriteLocalDateOnly(696, YieldRedemptionDate.Value);
			if (YieldRedemptionPrice is not null) writer.WriteNumber(697, YieldRedemptionPrice.Value);
			if (YieldRedemptionPriceType is not null) writer.WriteWholeNumber(698, YieldRedemptionPriceType.Value);
		}
	}
}
