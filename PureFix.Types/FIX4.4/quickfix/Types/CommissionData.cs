using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CommissionData : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 0, Required = false)]
		public double? Commission { get; set; }
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 1, Required = false)]
		public string? CommType { get; set; }
		
		[TagDetails(Tag = 479, Type = TagType.String, Offset = 2, Required = false)]
		public string? CommCurrency { get; set; }
		
		[TagDetails(Tag = 497, Type = TagType.String, Offset = 3, Required = false)]
		public string? FundRenewWaiv { get; set; }
		
	}
}
