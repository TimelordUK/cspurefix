using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("o")]
	public sealed class RegistrationInstructions : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(513, TagType.String)]
		public string? RegistID { get; set; }
		
		[TagDetails(514, TagType.String)]
		public string? RegistTransType { get; set; }
		
		[TagDetails(508, TagType.String)]
		public string? RegistRefID { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(493, TagType.String)]
		public string? RegistAcctType { get; set; }
		
		[TagDetails(495, TagType.Int)]
		public int? TaxAdvantageType { get; set; }
		
		[TagDetails(517, TagType.String)]
		public string? OwnershipType { get; set; }
		
		[Component]
		public RgstDtlsGrp? RgstDtlsGrp { get; set; }
		
		[Component]
		public RgstDistInstGrp? RgstDistInstGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
