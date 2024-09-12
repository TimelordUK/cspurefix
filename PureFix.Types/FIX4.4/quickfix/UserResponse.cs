using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BF", FixVersion.FIX44)]
	public sealed class UserResponse : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 923, Type = TagType.String, Offset = 1)]
		public string? UserRequestID { get; set; }
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 2)]
		public string? Username { get; set; }
		
		[TagDetails(Tag = 926, Type = TagType.Int, Offset = 3)]
		public int? UserStatus { get; set; }
		
		[TagDetails(Tag = 927, Type = TagType.String, Offset = 4)]
		public string? UserStatusText { get; set; }
		
		[Component(Offset = 5)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
