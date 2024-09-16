using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BD", FixVersion.FIX44)]
	public sealed partial class NetworkCounterpartySystemStatusResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 937, Type = TagType.Int, Offset = 1, Required = true)]
		public int? NetworkStatusResponseType { get; set; }
		
		[TagDetails(Tag = 933, Type = TagType.String, Offset = 2, Required = false)]
		public string? NetworkRequestID { get; set; }
		
		[TagDetails(Tag = 932, Type = TagType.String, Offset = 3, Required = true)]
		public string? NetworkResponseID { get; set; }
		
		[TagDetails(Tag = 934, Type = TagType.String, Offset = 4, Required = false)]
		public string? LastNetworkResponseID { get; set; }
		
		[Component(Offset = 5, Required = true)]
		public CompIDStatGrp? CompIDStatGrp { get; set; }
		
		[Component(Offset = 6, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& NetworkStatusResponseType is not null
				&& NetworkResponseID is not null
				&& CompIDStatGrp is not null && ((IFixValidator)CompIDStatGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (NetworkStatusResponseType is not null) writer.WriteWholeNumber(937, NetworkStatusResponseType.Value);
			if (NetworkRequestID is not null) writer.WriteString(933, NetworkRequestID);
			if (NetworkResponseID is not null) writer.WriteString(932, NetworkResponseID);
			if (LastNetworkResponseID is not null) writer.WriteString(934, LastNetworkResponseID);
			if (CompIDStatGrp is not null) ((IFixEncoder)CompIDStatGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
