using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BC", FixVersion.FIX44)]
	public sealed partial class NetworkCounterpartySystemStatusRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 935, Type = TagType.Int, Offset = 1, Required = true)]
		public int? NetworkRequestType { get; set; }
		
		[TagDetails(Tag = 933, Type = TagType.String, Offset = 2, Required = true)]
		public string? NetworkRequestID { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public CompIDReqGrp? CompIDReqGrp { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& NetworkRequestType is not null
				&& NetworkRequestID is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (NetworkRequestType is not null) writer.WriteWholeNumber(935, NetworkRequestType.Value);
			if (NetworkRequestID is not null) writer.WriteString(933, NetworkRequestID);
			if (CompIDReqGrp is not null) ((IFixEncoder)CompIDReqGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
