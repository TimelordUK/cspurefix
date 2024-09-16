using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("2", FixVersion.FIX43)]
	public sealed partial class ResendRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 7, Type = TagType.Int, Offset = 1, Required = true)]
		public int? BeginSeqNo { get; set; }
		
		[TagDetails(Tag = 16, Type = TagType.Int, Offset = 2, Required = true)]
		public int? EndSeqNo { get; set; }
		
		[Component(Offset = 3, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& BeginSeqNo is not null
				&& EndSeqNo is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (BeginSeqNo is not null) writer.WriteWholeNumber(7, BeginSeqNo.Value);
			if (EndSeqNo is not null) writer.WriteWholeNumber(16, EndSeqNo.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
