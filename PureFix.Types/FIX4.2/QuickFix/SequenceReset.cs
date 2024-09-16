using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("4", FixVersion.FIX42)]
	public sealed partial class SequenceReset : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 123, Type = TagType.Boolean, Offset = 1, Required = false)]
		public bool? GapFillFlag { get; set; }
		
		[TagDetails(Tag = 36, Type = TagType.Int, Offset = 2, Required = true)]
		public int? NewSeqNo { get; set; }
		
		[Component(Offset = 3, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& NewSeqNo is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (GapFillFlag is not null) writer.WriteBoolean(123, GapFillFlag.Value);
			if (NewSeqNo is not null) writer.WriteWholeNumber(36, NewSeqNo.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
