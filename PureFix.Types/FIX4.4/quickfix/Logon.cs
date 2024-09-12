using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class Logon : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(98, TagType.Int)]
		public int? EncryptMethod { get; set; }
		
		[TagDetails(108, TagType.Int)]
		public int? HeartBtInt { get; set; }
		
		[TagDetails(95, TagType.Length)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(96, TagType.RawData)]
		public byte[]? RawData { get; set; }
		
		[TagDetails(141, TagType.Boolean)]
		public bool? ResetSeqNumFlag { get; set; }
		
		[TagDetails(789, TagType.Int)]
		public int? NextExpectedMsgSeqNum { get; set; }
		
		[TagDetails(383, TagType.Length)]
		public int? MaxMessageSize { get; set; }
		
		[Group]
		public NoMsgTypes? NoMsgTypes { get; set; }
		
		[TagDetails(464, TagType.Boolean)]
		public bool? TestMessageIndicator { get; set; }
		
		[TagDetails(553, TagType.String)]
		public string? Username { get; set; }
		
		[TagDetails(554, TagType.String)]
		public string? Password { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
