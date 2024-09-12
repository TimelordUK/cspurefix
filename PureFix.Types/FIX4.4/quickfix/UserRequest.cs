using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BE", FixVersion.FIX44)]
	public sealed class UserRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 923, Type = TagType.String, Offset = 1)]
		public string? UserRequestID { get; set; }
		
		[TagDetails(Tag = 924, Type = TagType.Int, Offset = 2)]
		public int? UserRequestType { get; set; }
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 3)]
		public string? Username { get; set; }
		
		[TagDetails(Tag = 554, Type = TagType.String, Offset = 4)]
		public string? Password { get; set; }
		
		[TagDetails(Tag = 925, Type = TagType.String, Offset = 5)]
		public string? NewPassword { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 6)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 7)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 8)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
