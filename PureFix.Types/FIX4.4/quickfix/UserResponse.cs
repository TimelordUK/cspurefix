using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class UserResponse : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(923, TagType.String)]
		public string? UserRequestID { get; set; }
		
		[TagDetails(553, TagType.String)]
		public string? Username { get; set; }
		
		[TagDetails(926, TagType.Int)]
		public int? UserStatus { get; set; }
		
		[TagDetails(927, TagType.String)]
		public string? UserStatusText { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
