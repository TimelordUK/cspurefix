using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegOrdGrpNoLegs : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(Tag = 687, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LegQty { get; set; }
		
		[TagDetails(Tag = 690, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegSwapType { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public LegStipulations? LegStipulations { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public LegPreAllocGrp? LegPreAllocGrp { get; set; }
		
		[TagDetails(Tag = 564, Type = TagType.String, Offset = 5, Required = false)]
		public string? LegPositionEffect { get; set; }
		
		[TagDetails(Tag = 565, Type = TagType.Int, Offset = 6, Required = false)]
		public int? LegCoveredOrUncovered { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(Tag = 654, Type = TagType.String, Offset = 8, Required = false)]
		public string? LegRefID { get; set; }
		
		[TagDetails(Tag = 566, Type = TagType.Float, Offset = 9, Required = false)]
		public double? LegPrice { get; set; }
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 10, Required = false)]
		public string? LegSettlType { get; set; }
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 11, Required = false)]
		public DateOnly? LegSettlDate { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegQty is not null) writer.WriteNumber(687, LegQty.Value);
			if (LegSwapType is not null) writer.WriteWholeNumber(690, LegSwapType.Value);
			if (LegStipulations is not null) ((IFixEncoder)LegStipulations).Encode(writer);
			if (LegPreAllocGrp is not null) ((IFixEncoder)LegPreAllocGrp).Encode(writer);
			if (LegPositionEffect is not null) writer.WriteString(564, LegPositionEffect);
			if (LegCoveredOrUncovered is not null) writer.WriteWholeNumber(565, LegCoveredOrUncovered.Value);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			if (LegRefID is not null) writer.WriteString(654, LegRefID);
			if (LegPrice is not null) writer.WriteNumber(566, LegPrice.Value);
			if (LegSettlType is not null) writer.WriteString(587, LegSettlType);
			if (LegSettlDate is not null) writer.WriteLocalDateOnly(588, LegSettlDate.Value);
		}
	}
}
