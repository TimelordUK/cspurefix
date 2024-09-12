using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ConfirmationRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(859)]
		public string? ConfirmReqID { get; set; } // STRING
		
		[TagDetails(773)]
		public int? ConfirmType { get; set; } // INT
		
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		[TagDetails(70)]
		public string? AllocID { get; set; } // STRING
		
		[TagDetails(793)]
		public string? SecondaryAllocID { get; set; } // STRING
		
		[TagDetails(467)]
		public string? IndividualAllocID { get; set; } // STRING
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(79)]
		public string? AllocAccount { get; set; } // STRING
		
		[TagDetails(661)]
		public int? AllocAcctIDSource { get; set; } // INT
		
		[TagDetails(798)]
		public int? AllocAccountType { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
