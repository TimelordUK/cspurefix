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
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 0)]
		public double? Commission { get; set; }
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 1)]
		public string? CommType { get; set; }
		
		[TagDetails(Tag = 479, Type = TagType.String, Offset = 2)]
		public string? CommCurrency { get; set; }
		
		[TagDetails(Tag = 497, Type = TagType.String, Offset = 3)]
		public string? FundRenewWaiv { get; set; }
		
	}
}
