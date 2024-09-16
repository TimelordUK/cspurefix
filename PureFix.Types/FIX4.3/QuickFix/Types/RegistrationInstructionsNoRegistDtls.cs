using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class RegistrationInstructionsNoRegistDtls : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 509, Type = TagType.String, Offset = 0, Required = false)]
		public string? RegistDetls { get; set; }
		
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
		public DateOnly? DateOfBirth { get; set; }
		
		[TagDetails(Tag = 475, Type = TagType.String, Offset = 7, Required = false)]
		public string? InvestorCountryOfResidence { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RegistDetls is not null) writer.WriteString(509, RegistDetls);
			if (RegistEmail is not null) writer.WriteString(511, RegistEmail);
			if (MailingDtls is not null) writer.WriteString(474, MailingDtls);
			if (MailingInst is not null) writer.WriteString(482, MailingInst);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			if (OwnerType is not null) writer.WriteWholeNumber(522, OwnerType.Value);
			if (DateOfBirth is not null) writer.WriteLocalDateOnly(486, DateOfBirth.Value);
			if (InvestorCountryOfResidence is not null) writer.WriteString(475, InvestorCountryOfResidence);
		}
	}
}
