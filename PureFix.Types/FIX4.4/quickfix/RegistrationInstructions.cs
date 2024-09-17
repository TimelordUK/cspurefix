using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("o", FixVersion.FIX44)]
	public sealed partial class RegistrationInstructions : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 1, Required = true)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 514, Type = TagType.String, Offset = 2, Required = true)]
		public string? RegistTransType {get; set;}
		
		[TagDetails(Tag = 508, Type = TagType.String, Offset = 3, Required = true)]
		public string? RegistRefID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 4, Required = false)]
		public string? ClOrdID {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 7, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 493, Type = TagType.String, Offset = 8, Required = false)]
		public string? RegistAcctType {get; set;}
		
		[TagDetails(Tag = 495, Type = TagType.Int, Offset = 9, Required = false)]
		public int? TaxAdvantageType {get; set;}
		
		[TagDetails(Tag = 517, Type = TagType.String, Offset = 10, Required = false)]
		public string? OwnershipType {get; set;}
		
		[Component(Offset = 11, Required = false)]
		public RgstDtlsGrpComponent? RgstDtlsGrp {get; set;}
		
		[Component(Offset = 12, Required = false)]
		public RgstDistInstGrpComponent? RgstDistInstGrp {get; set;}
		
		[Component(Offset = 13, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& RegistID is not null
				&& RegistTransType is not null
				&& RegistRefID is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
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
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (RegistAcctType is not null) writer.WriteString(493, RegistAcctType);
			if (TaxAdvantageType is not null) writer.WriteWholeNumber(495, TaxAdvantageType.Value);
			if (OwnershipType is not null) writer.WriteString(517, OwnershipType);
			if (RgstDtlsGrp is not null) ((IFixEncoder)RgstDtlsGrp).Encode(writer);
			if (RgstDistInstGrp is not null) ((IFixEncoder)RgstDistInstGrp).Encode(writer);
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
			AcctIDSource = view.GetInt32(660);
			RegistAcctType = view.GetString(493);
			TaxAdvantageType = view.GetInt32(495);
			OwnershipType = view.GetString(517);
			if (view.GetView("RgstDtlsGrp") is IMessageView viewRgstDtlsGrp)
			{
				RgstDtlsGrp = new();
				((IFixParser)RgstDtlsGrp).Parse(viewRgstDtlsGrp);
			}
			if (view.GetView("RgstDistInstGrp") is IMessageView viewRgstDistInstGrp)
			{
				RgstDistInstGrp = new();
				((IFixParser)RgstDistInstGrp).Parse(viewRgstDistInstGrp);
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
					value = StandardHeader;
					break;
				case "RegistID":
					value = RegistID;
					break;
				case "RegistTransType":
					value = RegistTransType;
					break;
				case "RegistRefID":
					value = RegistRefID;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "Parties":
					value = Parties;
					break;
				case "Account":
					value = Account;
					break;
				case "AcctIDSource":
					value = AcctIDSource;
					break;
				case "RegistAcctType":
					value = RegistAcctType;
					break;
				case "TaxAdvantageType":
					value = TaxAdvantageType;
					break;
				case "OwnershipType":
					value = OwnershipType;
					break;
				case "RgstDtlsGrp":
					value = RgstDtlsGrp;
					break;
				case "RgstDistInstGrp":
					value = RgstDistInstGrp;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
	}
}
