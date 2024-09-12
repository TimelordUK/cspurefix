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
		[TagDetails(Tag = 477, Type = TagType.Int, Offset = 0, Required = false)]
		public int? DistribPaymentMethod { get; set; }
		
		[TagDetails(Tag = 512, Type = TagType.Float, Offset = 1, Required = false)]
		public double? DistribPercentage { get; set; }
		
		[TagDetails(Tag = 478, Type = TagType.String, Offset = 2, Required = false)]
		public string? CashDistribCurr { get; set; }
		
		[TagDetails(Tag = 498, Type = TagType.String, Offset = 3, Required = false)]
		public string? CashDistribAgentName { get; set; }
		
		[TagDetails(Tag = 499, Type = TagType.String, Offset = 4, Required = false)]
		public string? CashDistribAgentCode { get; set; }
		
		[TagDetails(Tag = 500, Type = TagType.String, Offset = 5, Required = false)]
		public string? CashDistribAgentAcctNumber { get; set; }
		
		[TagDetails(Tag = 501, Type = TagType.String, Offset = 6, Required = false)]
		public string? CashDistribPayRef { get; set; }
		
		[TagDetails(Tag = 502, Type = TagType.String, Offset = 7, Required = false)]
		public string? CashDistribAgentAcctName { get; set; }
		
	}
}
