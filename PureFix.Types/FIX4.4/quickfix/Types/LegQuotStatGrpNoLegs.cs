using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegQuotStatGrpNoLegs : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLeg? InstrumentLeg { get; set; }
		
		[TagDetails(Tag = 687, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LegQty { get; set; }
		
		[TagDetails(Tag = 690, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegSwapType { get; set; }
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 3, Required = false)]
		public string? LegSettlType { get; set; }
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateOnly? LegSettlDate { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public LegStipulations? LegStipulations { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegQty is not null) writer.WriteNumber(687, LegQty.Value);
			if (LegSwapType is not null) writer.WriteWholeNumber(690, LegSwapType.Value);
			if (LegSettlType is not null) writer.WriteString(587, LegSettlType);
			if (LegSettlDate is not null) writer.WriteLocalDateOnly(588, LegSettlDate.Value);
			if (LegStipulations is not null) ((IFixEncoder)LegStipulations).Encode(writer);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
		}
	}
}
