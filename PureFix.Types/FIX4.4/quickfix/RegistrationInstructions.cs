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
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 1, Required = true)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 514, Type = TagType.String, Offset = 2, Required = true)]
		public string? RegistTransType { get; set; }
		
		[TagDetails(Tag = 508, Type = TagType.String, Offset = 3, Required = true)]
		public string? RegistRefID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 4, Required = false)]
		public string? ClOrdID { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 7, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 493, Type = TagType.String, Offset = 8, Required = false)]
		public string? RegistAcctType { get; set; }
		
		[TagDetails(Tag = 495, Type = TagType.Int, Offset = 9, Required = false)]
		public int? TaxAdvantageType { get; set; }
		
		[TagDetails(Tag = 517, Type = TagType.String, Offset = 10, Required = false)]
		public string? OwnershipType { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public RgstDtlsGrp? RgstDtlsGrp { get; set; }
		
		[Component(Offset = 12, Required = false)]
		public RgstDistInstGrp? RgstDistInstGrp { get; set; }
		
		[Component(Offset = 13, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
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
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
