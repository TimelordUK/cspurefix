using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("p", FixVersion.FIX44)]
	public sealed partial class RegistrationInstructionsResponse : IFixMessage
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
		
		[TagDetails(Tag = 506, Type = TagType.String, Offset = 8, Required = true)]
		public string? RegistStatus { get; set; }
		
		[TagDetails(Tag = 507, Type = TagType.Int, Offset = 9, Required = false)]
		public int? RegistRejReasonCode { get; set; }
		
		[TagDetails(Tag = 496, Type = TagType.String, Offset = 10, Required = false)]
		public string? RegistRejReasonText { get; set; }
		
		[Component(Offset = 11, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& RegistID is not null
				&& RegistTransType is not null
				&& RegistRefID is not null
				&& RegistStatus is not null
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
			if (RegistStatus is not null) writer.WriteString(506, RegistStatus);
			if (RegistRejReasonCode is not null) writer.WriteWholeNumber(507, RegistRejReasonCode.Value);
			if (RegistRejReasonText is not null) writer.WriteString(496, RegistRejReasonText);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
