using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class NewOrderMultilegNoAllocs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 1, Required = false)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 2, Required = false)]
		public double? AllocQty { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
			if (AllocQty is not null) writer.WriteNumber(80, AllocQty.Value);
		}
	}
}
