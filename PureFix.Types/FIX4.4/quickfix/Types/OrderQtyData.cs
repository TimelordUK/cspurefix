using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class OrderQtyData
	{
		[TagDetails(38, TagType.Float)]
		public double? OrderQty { get; set; }
		
		[TagDetails(152, TagType.Float)]
		public double? CashOrderQty { get; set; }
		
		[TagDetails(516, TagType.Float)]
		public double? OrderPercent { get; set; }
		
		[TagDetails(468, TagType.String)]
		public string? RoundingDirection { get; set; }
		
		[TagDetails(469, TagType.Float)]
		public double? RoundingModulus { get; set; }
		
	}
}
