using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class NewOrderSingleNoAllocs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 1, Required = false)]
		public double? AllocShares { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (AllocShares is not null) writer.WriteNumber(80, AllocShares.Value);
		}
	}
}
