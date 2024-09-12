using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("A", FixVersion.FIX44)]
	public sealed class Logon : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 98, Type = TagType.Int, Offset = 1, Required = true)]
		public int? EncryptMethod { get; set; }
		
		[TagDetails(Tag = 108, Type = TagType.Int, Offset = 2, Required = true)]
		public int? HeartBtInt { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 3, Required = false)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 4, Required = false)]
		public byte[]? RawData { get; set; }
		
		[TagDetails(Tag = 141, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? ResetSeqNumFlag { get; set; }
		
		[TagDetails(Tag = 789, Type = TagType.Int, Offset = 6, Required = false)]
		public int? NextExpectedMsgSeqNum { get; set; }
		
		[TagDetails(Tag = 383, Type = TagType.Length, Offset = 7, Required = false)]
		public int? MaxMessageSize { get; set; }
		
		[Group(NoOfTag = 384, Offset = 8, Required = false)]
		public NoMsgTypes[]? NoMsgTypes { get; set; }
		
		[TagDetails(Tag = 464, Type = TagType.Boolean, Offset = 9, Required = false)]
		public bool? TestMessageIndicator { get; set; }
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 10, Required = false)]
		public string? Username { get; set; }
		
		[TagDetails(Tag = 554, Type = TagType.String, Offset = 11, Required = false)]
		public string? Password { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
