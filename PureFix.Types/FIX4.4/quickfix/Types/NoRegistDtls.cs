using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoRegistDtls
	{
		[TagDetails(509, TagType.String)]
		public string? RegistDtls { get; set; }
		
		[TagDetails(511, TagType.String)]
		public string? RegistEmail { get; set; }
		
		[TagDetails(474, TagType.String)]
		public string? MailingDtls { get; set; }
		
		[TagDetails(482, TagType.String)]
		public string? MailingInst { get; set; }
		
		[Component]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(522, TagType.Int)]
		public int? OwnerType { get; set; }
		
		[TagDetails(486, TagType.LocalDate)]
		public DateTime? DateOfBirth { get; set; }
		
		[TagDetails(475, TagType.String)]
		public string? InvestorCountryOfResidence { get; set; }
		
	}
}
