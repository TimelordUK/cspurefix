using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class CommissionData
	{
		[TagDetails(12, TagType.Float)]
		public double? Commission { get; set; }
		
		[TagDetails(13, TagType.String)]
		public string? CommType { get; set; }
		
		[TagDetails(479, TagType.String)]
		public string? CommCurrency { get; set; }
		
		[TagDetails(497, TagType.String)]
		public string? FundRenewWaiv { get; set; }
		
	}
}
