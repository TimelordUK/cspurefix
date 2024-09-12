using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoDistribInsts
	{
		[TagDetails(477, TagType.Int)]
		public int? DistribPaymentMethod { get; set; }
		
		[TagDetails(512, TagType.Float)]
		public double? DistribPercentage { get; set; }
		
		[TagDetails(478, TagType.String)]
		public string? CashDistribCurr { get; set; }
		
		[TagDetails(498, TagType.String)]
		public string? CashDistribAgentName { get; set; }
		
		[TagDetails(499, TagType.String)]
		public string? CashDistribAgentCode { get; set; }
		
		[TagDetails(500, TagType.String)]
		public string? CashDistribAgentAcctNumber { get; set; }
		
		[TagDetails(501, TagType.String)]
		public string? CashDistribPayRef { get; set; }
		
		[TagDetails(502, TagType.String)]
		public string? CashDistribAgentAcctName { get; set; }
		
	}
}
