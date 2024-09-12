using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class RegistrationInstructions : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? RegistID { get; set; } // 513 STRING
		public string? RegistTransType { get; set; } // 514 CHAR
		public string? RegistRefID { get; set; } // 508 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public string? RegistAcctType { get; set; } // 493 STRING
		public int? TaxAdvantageType { get; set; } // 495 INT
		public string? OwnershipType { get; set; } // 517 CHAR
		public RgstDtlsGrp? RgstDtlsGrp { get; set; }
		public RgstDistInstGrp? RgstDistInstGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
