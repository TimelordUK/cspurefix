using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("X", FixVersion.FIX44)]
	public sealed partial class MarketDataIncrementalRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = false)]
		public string? MDReqID { get; set; }
		
		[Component(Offset = 2, Required = true)]
		public MDIncGrp? MDIncGrp { get; set; }
		
		[TagDetails(Tag = 813, Type = TagType.Int, Offset = 3, Required = false)]
		public int? ApplQueueDepth { get; set; }
		
		[TagDetails(Tag = 814, Type = TagType.Int, Offset = 4, Required = false)]
		public int? ApplQueueResolution { get; set; }
		
		[Component(Offset = 5, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& MDIncGrp is not null && ((IFixValidator)MDIncGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
			if (MDIncGrp is not null) ((IFixEncoder)MDIncGrp).Encode(writer);
			if (ApplQueueDepth is not null) writer.WriteWholeNumber(813, ApplQueueDepth.Value);
			if (ApplQueueResolution is not null) writer.WriteWholeNumber(814, ApplQueueResolution.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
