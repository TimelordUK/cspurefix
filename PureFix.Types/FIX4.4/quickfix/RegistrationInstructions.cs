using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("o", FixVersion.FIX44)]
	public sealed class RegistrationInstructions : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 1, Required = true)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 514, Type = TagType.String, Offset = 2, Required = true)]
		public string? RegistTransType { get; set; }
		
		[TagDetails(Tag = 508, Type = TagType.String, Offset = 3, Required = true)]
		public string? RegistRefID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 4, Required = false)]
		public string? ClOrdID { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 7, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 493, Type = TagType.String, Offset = 8, Required = false)]
		public string? RegistAcctType { get; set; }
		
		[TagDetails(Tag = 495, Type = TagType.Int, Offset = 9, Required = false)]
		public int? TaxAdvantageType { get; set; }
		
		[TagDetails(Tag = 517, Type = TagType.String, Offset = 10, Required = false)]
		public string? OwnershipType { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public RgstDtlsGrp? RgstDtlsGrp { get; set; }
		
		[Component(Offset = 12, Required = false)]
		public RgstDistInstGrp? RgstDistInstGrp { get; set; }
		
		[Component(Offset = 13, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
