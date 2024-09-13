using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BD", FixVersion.FIX44)]
	public sealed class NetworkCounterpartySystemStatusResponse : IFixMessage
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
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
