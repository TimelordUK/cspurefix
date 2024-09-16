using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class NewOrderMultilegNoLegs : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(Tag = 564, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegPositionEffect { get; set; }
		
		[TagDetails(Tag = 565, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegCoveredOrUncovered { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(Tag = 654, Type = TagType.String, Offset = 4, Required = false)]
		public string? LegRefID { get; set; }
		
		[TagDetails(Tag = 566, Type = TagType.Float, Offset = 5, Required = false)]
		public double? LegPrice { get; set; }
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 6, Required = false)]
		public string? LegSettlmntTyp { get; set; }
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 7, Required = false)]
		public DateOnly? LegFutSettDate { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegPositionEffect is not null) writer.WriteString(564, LegPositionEffect);
			if (LegCoveredOrUncovered is not null) writer.WriteWholeNumber(565, LegCoveredOrUncovered.Value);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			if (LegRefID is not null) writer.WriteString(654, LegRefID);
			if (LegPrice is not null) writer.WriteNumber(566, LegPrice.Value);
			if (LegSettlmntTyp is not null) writer.WriteString(587, LegSettlmntTyp);
			if (LegFutSettDate is not null) writer.WriteLocalDateOnly(588, LegFutSettDate.Value);
		}
	}
}
