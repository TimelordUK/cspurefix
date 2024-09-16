using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("y", FixVersion.FIX44)]
	public sealed partial class SecurityList : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2, Required = true)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(Tag = 560, Type = TagType.Int, Offset = 3, Required = true)]
		public int? SecurityRequestResult { get; set; }
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TotNoRelatedSym { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public SecListGrp? SecListGrp { get; set; }
		
		[Component(Offset = 7, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SecurityReqID is not null
				&& SecurityResponseID is not null
				&& SecurityRequestResult is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityResponseID is not null) writer.WriteString(322, SecurityResponseID);
			if (SecurityRequestResult is not null) writer.WriteWholeNumber(560, SecurityRequestResult.Value);
			if (TotNoRelatedSym is not null) writer.WriteWholeNumber(393, TotNoRelatedSym.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (SecListGrp is not null) ((IFixEncoder)SecListGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
