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
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(923)]
		public string? UserRequestID { get; set; } // STRING
		
		[TagDetails(553)]
		public string? Username { get; set; } // STRING
		
		[TagDetails(926)]
		public int? UserStatus { get; set; } // INT
		
		[TagDetails(927)]
		public string? UserStatusText { get; set; } // STRING
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
