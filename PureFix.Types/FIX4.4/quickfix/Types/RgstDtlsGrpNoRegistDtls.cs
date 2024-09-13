using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class RgstDtlsGrpNoRegistDtls
	{
		[TagDetails(Tag = 509, Type = TagType.String, Offset = 0, Required = false)]
		public string? RegistDtls { get; set; }
		
		[TagDetails(Tag = 511, Type = TagType.String, Offset = 1, Required = false)]
		public string? RegistEmail { get; set; }
		
		[TagDetails(Tag = 474, Type = TagType.String, Offset = 2, Required = false)]
		public string? MailingDtls { get; set; }
		
		[TagDetails(Tag = 482, Type = TagType.String, Offset = 3, Required = false)]
		public string? MailingInst { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(Tag = 522, Type = TagType.Int, Offset = 5, Required = false)]
		public int? OwnerType { get; set; }
		
		[TagDetails(Tag = 486, Type = TagType.LocalDate, Offset = 6, Required = false)]
		public DateTime? DateOfBirth { get; set; }
		
		[TagDetails(Tag = 475, Type = TagType.String, Offset = 7, Required = false)]
		public string? InvestorCountryOfResidence { get; set; }
		
	}
}
