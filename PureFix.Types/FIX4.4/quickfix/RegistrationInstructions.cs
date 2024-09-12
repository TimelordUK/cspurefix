using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class RegistrationInstructions : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(513)]
		public string? RegistID { get; set; } // STRING
		
		[TagDetails(514)]
		public string? RegistTransType { get; set; } // CHAR
		
		[TagDetails(508)]
		public string? RegistRefID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(493)]
		public string? RegistAcctType { get; set; } // STRING
		
		[TagDetails(495)]
		public int? TaxAdvantageType { get; set; } // INT
		
		[TagDetails(517)]
		public string? OwnershipType { get; set; } // CHAR
		
		public RgstDtlsGrp? RgstDtlsGrp { get; set; }
		public RgstDistInstGrp? RgstDistInstGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
