using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class UserRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(923)]
		public string? UserRequestID { get; set; } // STRING
		
		[TagDetails(924)]
		public int? UserRequestType { get; set; } // INT
		
		[TagDetails(553)]
		public string? Username { get; set; } // STRING
		
		[TagDetails(554)]
		public string? Password { get; set; } // STRING
		
		[TagDetails(925)]
		public string? NewPassword { get; set; } // STRING
		
		[TagDetails(95)]
		public int? RawDataLength { get; set; } // LENGTH
		
		[TagDetails(96)]
		public byte[]? RawData { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
