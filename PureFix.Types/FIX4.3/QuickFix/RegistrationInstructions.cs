using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("o", FixVersion.FIX43)]
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
		
		[TagDetails(Tag = 493, Type = TagType.String, Offset = 7, Required = false)]
		public string? RegistAcctType { get; set; }
		
		[TagDetails(Tag = 495, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TaxAdvantageType { get; set; }
		
		[TagDetails(Tag = 517, Type = TagType.String, Offset = 9, Required = false)]
		public string? OwnershipType { get; set; }
		
		[Group(NoOfTag = 473, Offset = 10, Required = false)]
		public RegistrationInstructionsNoRegistDtls[]? NoRegistDtls { get; set; }
		
		[Group(NoOfTag = 510, Offset = 11, Required = false)]
		public RegistrationInstructionsNoDistribInsts[]? NoDistribInsts { get; set; }
		
		[Component(Offset = 12, Required = true)]
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
			if (RegistAcctType is not null) writer.WriteString(493, RegistAcctType);
			if (TaxAdvantageType is not null) writer.WriteWholeNumber(495, TaxAdvantageType.Value);
			if (OwnershipType is not null) writer.WriteString(517, OwnershipType);
			if (NoRegistDtls is not null && NoRegistDtls.Length != 0)
			{
				writer.WriteWholeNumber(473, NoRegistDtls.Length);
				for (int i = 0; i < NoRegistDtls.Length; i++)
				{
					((IFixEncoder)NoRegistDtls[i]).Encode(writer);
				}
			}
			if (NoDistribInsts is not null && NoDistribInsts.Length != 0)
			{
				writer.WriteWholeNumber(510, NoDistribInsts.Length);
				for (int i = 0; i < NoDistribInsts.Length; i++)
				{
					((IFixEncoder)NoDistribInsts[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
