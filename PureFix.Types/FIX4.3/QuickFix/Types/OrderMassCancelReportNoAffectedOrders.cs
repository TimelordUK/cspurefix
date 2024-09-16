using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class OrderMassCancelReportNoAffectedOrders : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 0, Required = false)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 535, Type = TagType.String, Offset = 1, Required = false)]
		public string? AffectedOrderID { get; set; }
		
		[TagDetails(Tag = 536, Type = TagType.String, Offset = 2, Required = false)]
		public string? AffectedSecondaryOrderID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (AffectedOrderID is not null) writer.WriteString(535, AffectedOrderID);
			if (AffectedSecondaryOrderID is not null) writer.WriteString(536, AffectedSecondaryOrderID);
		}
	}
}
