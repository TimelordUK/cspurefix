using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BE", FixVersion.FIX44)]
	public sealed partial class UserRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 923, Type = TagType.String, Offset = 1, Required = true)]
		public string? UserRequestID { get; set; }
		
		[TagDetails(Tag = 924, Type = TagType.Int, Offset = 2, Required = true)]
		public int? UserRequestType { get; set; }
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 3, Required = true)]
		public string? Username { get; set; }
		
		[TagDetails(Tag = 554, Type = TagType.String, Offset = 4, Required = false)]
		public string? Password { get; set; }
		
		[TagDetails(Tag = 925, Type = TagType.String, Offset = 5, Required = false)]
		public string? NewPassword { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 6, Required = false, LinksToTag = 96)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 7, Required = false, LinksToTag = 95)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
