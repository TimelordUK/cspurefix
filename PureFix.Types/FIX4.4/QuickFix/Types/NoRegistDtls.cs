using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NoRegistDtls : IFixGroup
	{
		[TagDetails(Tag = 509, Type = TagType.String, Offset = 0, Required = false)]
		public string? RegistDtls {get; set;}
		
		[TagDetails(Tag = 511, Type = TagType.String, Offset = 1, Required = false)]
		public string? RegistEmail {get; set;}
		
		[TagDetails(Tag = 474, Type = TagType.String, Offset = 2, Required = false)]
		public string? MailingDtls {get; set;}
		
		[TagDetails(Tag = 482, Type = TagType.String, Offset = 3, Required = false)]
		public string? MailingInst {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public NestedPartiesComponent? NestedParties {get; set;}
		
		[TagDetails(Tag = 522, Type = TagType.Int, Offset = 5, Required = false)]
		public int? OwnerType {get; set;}
		
		[TagDetails(Tag = 486, Type = TagType.LocalDate, Offset = 6, Required = false)]
		public DateOnly? DateOfBirth {get; set;}
		
		[TagDetails(Tag = 475, Type = TagType.String, Offset = 7, Required = false)]
		public string? InvestorCountryOfResidence {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RegistDtls is not null) writer.WriteString(509, RegistDtls);
			if (RegistEmail is not null) writer.WriteString(511, RegistEmail);
			if (MailingDtls is not null) writer.WriteString(474, MailingDtls);
			if (MailingInst is not null) writer.WriteString(482, MailingInst);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			if (OwnerType is not null) writer.WriteWholeNumber(522, OwnerType.Value);
			if (DateOfBirth is not null) writer.WriteLocalDateOnly(486, DateOfBirth.Value);
			if (InvestorCountryOfResidence is not null) writer.WriteString(475, InvestorCountryOfResidence);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RegistDtls = view.GetString(509);
			RegistEmail = view.GetString(511);
			MailingDtls = view.GetString(474);
			MailingInst = view.GetString(482);
			if (view.GetView("NestedParties") is IMessageView viewNestedParties)
			{
				NestedParties = new();
				((IFixParser)NestedParties).Parse(viewNestedParties);
			}
			OwnerType = view.GetInt32(522);
			DateOfBirth = view.GetDateOnly(486);
			InvestorCountryOfResidence = view.GetString(475);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RegistDtls":
					value = RegistDtls;
					break;
				case "RegistEmail":
					value = RegistEmail;
					break;
				case "MailingDtls":
					value = MailingDtls;
					break;
				case "MailingInst":
					value = MailingInst;
					break;
				case "NestedParties":
					value = NestedParties;
					break;
				case "OwnerType":
					value = OwnerType;
					break;
				case "DateOfBirth":
					value = DateOfBirth;
					break;
				case "InvestorCountryOfResidence":
					value = InvestorCountryOfResidence;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RegistDtls = null;
			RegistEmail = null;
			MailingDtls = null;
			MailingInst = null;
			((IFixReset?)NestedParties)?.Reset();
			OwnerType = null;
			DateOfBirth = null;
			InvestorCountryOfResidence = null;
		}
	}
}
