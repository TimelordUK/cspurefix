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
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(923, TagType.String)]
		public string? UserRequestID { get; set; }
		
		[TagDetails(924, TagType.Int)]
		public int? UserRequestType { get; set; }
		
		[TagDetails(553, TagType.String)]
		public string? Username { get; set; }
		
		[TagDetails(554, TagType.String)]
		public string? Password { get; set; }
		
		[TagDetails(925, TagType.String)]
		public string? NewPassword { get; set; }
		
		[TagDetails(95, TagType.Length)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(96, TagType.RawData)]
		public byte[]? RawData { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
