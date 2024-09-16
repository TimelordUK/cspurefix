using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AH", FixVersion.FIX44)]
	public sealed partial class RFQRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 1, Required = true)]
		public string? RFQReqID { get; set; }
		
		[Component(Offset = 2, Required = true)]
		public RFQReqGrp? RFQReqGrp { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& RFQReqID is not null
				&& RFQReqGrp is not null && ((IFixValidator)RFQReqGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (RFQReqID is not null) writer.WriteString(644, RFQReqID);
			if (RFQReqGrp is not null) ((IFixEncoder)RFQReqGrp).Encode(writer);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
