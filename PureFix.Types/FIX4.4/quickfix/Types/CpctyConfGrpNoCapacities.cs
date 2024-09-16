using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CpctyConfGrpNoCapacities : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 0, Required = true)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 863, Type = TagType.Float, Offset = 2, Required = true)]
		public double? OrderCapacityQty { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				OrderCapacity is not null
				&& OrderCapacityQty is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (OrderCapacityQty is not null) writer.WriteNumber(863, OrderCapacityQty.Value);
		}
	}
}
