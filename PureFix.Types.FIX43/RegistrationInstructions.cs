using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("o", FixVersion.FIX43)]
	public sealed partial class RegistrationInstructions : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 1, Required = true)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 514, Type = TagType.String, Offset = 2, Required = true)]
		public string? RegistTransType {get; set;}
		
		[TagDetails(Tag = 508, Type = TagType.String, Offset = 3, Required = true)]
		public string? RegistRefID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 4, Required = false)]
		public string? ClOrdID {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public Parties? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 493, Type = TagType.String, Offset = 7, Required = false)]
		public string? RegistAcctType {get; set;}
		
		[TagDetails(Tag = 495, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TaxAdvantageType {get; set;}
		
		[TagDetails(Tag = 517, Type = TagType.String, Offset = 9, Required = false)]
		public string? OwnershipType {get; set;}
		
		public sealed partial class NoRegistDtls : IFixGroup
		{
			[TagDetails(Tag = 509, Type = TagType.String, Offset = 0, Required = false)]
			public string? RegistDetls {get; set;}
			
			[TagDetails(Tag = 511, Type = TagType.String, Offset = 1, Required = false)]
			public string? RegistEmail {get; set;}
			
			[TagDetails(Tag = 474, Type = TagType.String, Offset = 2, Required = false)]
			public string? MailingDtls {get; set;}
			
			[TagDetails(Tag = 482, Type = TagType.String, Offset = 3, Required = false)]
			public string? MailingInst {get; set;}
			
			[Component(Offset = 4, Required = false)]
			public NestedParties? NestedParties {get; set;}
			
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
				if (RegistDetls is not null) writer.WriteString(509, RegistDetls);
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
				
				RegistDetls = view.GetString(509);
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
					case "RegistDetls":
					{
						value = RegistDetls;
						break;
					}
					case "RegistEmail":
					{
						value = RegistEmail;
						break;
					}
					case "MailingDtls":
					{
						value = MailingDtls;
						break;
					}
					case "MailingInst":
					{
						value = MailingInst;
						break;
					}
					case "NestedParties":
					{
						value = NestedParties;
						break;
					}
					case "OwnerType":
					{
						value = OwnerType;
						break;
					}
					case "DateOfBirth":
					{
						value = DateOfBirth;
						break;
					}
					case "InvestorCountryOfResidence":
					{
						value = InvestorCountryOfResidence;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				RegistDetls = null;
				RegistEmail = null;
				MailingDtls = null;
				MailingInst = null;
				((IFixReset?)NestedParties)?.Reset();
				OwnerType = null;
				DateOfBirth = null;
				InvestorCountryOfResidence = null;
			}
		}
		[Group(NoOfTag = 473, Offset = 10, Required = false)]
		public NoRegistDtls[]? RegistDtls {get; set;}
		
		public sealed partial class NoDistribInsts : IFixGroup
		{
			[TagDetails(Tag = 477, Type = TagType.Int, Offset = 0, Required = false)]
			public int? DistribPaymentMethod {get; set;}
			
			[TagDetails(Tag = 512, Type = TagType.Float, Offset = 1, Required = false)]
			public double? DistribPercentage {get; set;}
			
			[TagDetails(Tag = 478, Type = TagType.String, Offset = 2, Required = false)]
			public string? CashDistribCurr {get; set;}
			
			[TagDetails(Tag = 498, Type = TagType.String, Offset = 3, Required = false)]
			public string? CashDistribAgentName {get; set;}
			
			[TagDetails(Tag = 499, Type = TagType.String, Offset = 4, Required = false)]
			public string? CashDistribAgentCode {get; set;}
			
			[TagDetails(Tag = 500, Type = TagType.String, Offset = 5, Required = false)]
			public string? CashDistribAgentAcctNumber {get; set;}
			
			[TagDetails(Tag = 501, Type = TagType.String, Offset = 6, Required = false)]
			public string? CashDistribPayRef {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (DistribPaymentMethod is not null) writer.WriteWholeNumber(477, DistribPaymentMethod.Value);
				if (DistribPercentage is not null) writer.WriteNumber(512, DistribPercentage.Value);
				if (CashDistribCurr is not null) writer.WriteString(478, CashDistribCurr);
				if (CashDistribAgentName is not null) writer.WriteString(498, CashDistribAgentName);
				if (CashDistribAgentCode is not null) writer.WriteString(499, CashDistribAgentCode);
				if (CashDistribAgentAcctNumber is not null) writer.WriteString(500, CashDistribAgentAcctNumber);
				if (CashDistribPayRef is not null) writer.WriteString(501, CashDistribPayRef);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				DistribPaymentMethod = view.GetInt32(477);
				DistribPercentage = view.GetDouble(512);
				CashDistribCurr = view.GetString(478);
				CashDistribAgentName = view.GetString(498);
				CashDistribAgentCode = view.GetString(499);
				CashDistribAgentAcctNumber = view.GetString(500);
				CashDistribPayRef = view.GetString(501);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "DistribPaymentMethod":
					{
						value = DistribPaymentMethod;
						break;
					}
					case "DistribPercentage":
					{
						value = DistribPercentage;
						break;
					}
					case "CashDistribCurr":
					{
						value = CashDistribCurr;
						break;
					}
					case "CashDistribAgentName":
					{
						value = CashDistribAgentName;
						break;
					}
					case "CashDistribAgentCode":
					{
						value = CashDistribAgentCode;
						break;
					}
					case "CashDistribAgentAcctNumber":
					{
						value = CashDistribAgentAcctNumber;
						break;
					}
					case "CashDistribPayRef":
					{
						value = CashDistribPayRef;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				DistribPaymentMethod = null;
				DistribPercentage = null;
				CashDistribCurr = null;
				CashDistribAgentName = null;
				CashDistribAgentCode = null;
				CashDistribAgentAcctNumber = null;
				CashDistribPayRef = null;
			}
		}
		[Group(NoOfTag = 510, Offset = 11, Required = false)]
		public NoDistribInsts[]? DistribInsts {get; set;}
		
		[Component(Offset = 12, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (RegistID is not null) writer.WriteString(513, RegistID);
			if (RegistTransType is not null) writer.WriteString(514, RegistTransType);
			if (RegistRefID is not null) writer.WriteString(508, RegistRefID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (RegistAcctType is not null) writer.WriteString(493, RegistAcctType);
			if (TaxAdvantageType is not null) writer.WriteWholeNumber(495, TaxAdvantageType.Value);
			if (OwnershipType is not null) writer.WriteString(517, OwnershipType);
			if (RegistDtls is not null && RegistDtls.Length != 0)
			{
				writer.WriteWholeNumber(473, RegistDtls.Length);
				for (int i = 0; i < RegistDtls.Length; i++)
				{
					((IFixEncoder)RegistDtls[i]).Encode(writer);
				}
			}
			if (DistribInsts is not null && DistribInsts.Length != 0)
			{
				writer.WriteWholeNumber(510, DistribInsts.Length);
				for (int i = 0; i < DistribInsts.Length; i++)
				{
					((IFixEncoder)DistribInsts[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			RegistID = view.GetString(513);
			RegistTransType = view.GetString(514);
			RegistRefID = view.GetString(508);
			ClOrdID = view.GetString(11);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			Account = view.GetString(1);
			RegistAcctType = view.GetString(493);
			TaxAdvantageType = view.GetInt32(495);
			OwnershipType = view.GetString(517);
			if (view.GetView("NoRegistDtls") is IMessageView viewNoRegistDtls)
			{
				var count = viewNoRegistDtls.GroupCount();
				RegistDtls = new NoRegistDtls[count];
				for (int i = 0; i < count; i++)
				{
					RegistDtls[i] = new();
					((IFixParser)RegistDtls[i]).Parse(viewNoRegistDtls.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoDistribInsts") is IMessageView viewNoDistribInsts)
			{
				var count = viewNoDistribInsts.GroupCount();
				DistribInsts = new NoDistribInsts[count];
				for (int i = 0; i < count; i++)
				{
					DistribInsts[i] = new();
					((IFixParser)DistribInsts[i]).Parse(viewNoDistribInsts.GetGroupInstance(i));
				}
			}
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
				{
					value = StandardHeader;
					break;
				}
				case "RegistID":
				{
					value = RegistID;
					break;
				}
				case "RegistTransType":
				{
					value = RegistTransType;
					break;
				}
				case "RegistRefID":
				{
					value = RegistRefID;
					break;
				}
				case "ClOrdID":
				{
					value = ClOrdID;
					break;
				}
				case "Parties":
				{
					value = Parties;
					break;
				}
				case "Account":
				{
					value = Account;
					break;
				}
				case "RegistAcctType":
				{
					value = RegistAcctType;
					break;
				}
				case "TaxAdvantageType":
				{
					value = TaxAdvantageType;
					break;
				}
				case "OwnershipType":
				{
					value = OwnershipType;
					break;
				}
				case "NoRegistDtls":
				{
					value = RegistDtls;
					break;
				}
				case "NoDistribInsts":
				{
					value = DistribInsts;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			RegistID = null;
			RegistTransType = null;
			RegistRefID = null;
			ClOrdID = null;
			((IFixReset?)Parties)?.Reset();
			Account = null;
			RegistAcctType = null;
			TaxAdvantageType = null;
			OwnershipType = null;
			RegistDtls = null;
			DistribInsts = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
