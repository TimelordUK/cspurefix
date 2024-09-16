using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class OrderQtyData : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 38, Type = TagType.Float, Offset = 0, Required = false)]
		public double? OrderQty { get; set; }
		
		[TagDetails(Tag = 152, Type = TagType.Float, Offset = 1, Required = false)]
		public double? CashOrderQty { get; set; }
		
		[TagDetails(Tag = 516, Type = TagType.Float, Offset = 2, Required = false)]
		public double? OrderPercent { get; set; }
		
		[TagDetails(Tag = 468, Type = TagType.String, Offset = 3, Required = false)]
		public string? RoundingDirection { get; set; }
		
		[TagDetails(Tag = 469, Type = TagType.Float, Offset = 4, Required = false)]
		public double? RoundingModulus { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
			if (CashOrderQty is not null) writer.WriteNumber(152, CashOrderQty.Value);
			if (OrderPercent is not null) writer.WriteNumber(516, OrderPercent.Value);
			if (RoundingDirection is not null) writer.WriteString(468, RoundingDirection);
			if (RoundingModulus is not null) writer.WriteNumber(469, RoundingModulus.Value);
		}
	}
}
