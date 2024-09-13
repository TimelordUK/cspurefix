using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("m", FixVersion.FIX44)]
	public sealed class ListStrikePrice : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 422, Type = TagType.Int, Offset = 2, Required = true)]
		public int? TotNoStrikes { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 3, Required = false)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public InstrmtStrkPxGrp? InstrmtStrkPxGrp { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public UndInstrmtStrkPxGrp? UndInstrmtStrkPxGrp { get; set; }
		
		[Component(Offset = 6, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
