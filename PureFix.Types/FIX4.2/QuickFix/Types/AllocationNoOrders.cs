using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class AllocationNoOrders : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 3, Required = false)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 105, Type = TagType.String, Offset = 4, Required = false)]
		public string? WaveNo { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (WaveNo is not null) writer.WriteString(105, WaveNo);
		}
	}
}
