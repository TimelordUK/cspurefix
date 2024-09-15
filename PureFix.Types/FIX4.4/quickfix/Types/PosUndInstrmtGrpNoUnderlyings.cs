using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PosUndInstrmtGrpNoUnderlyings : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 732, Type = TagType.Float, Offset = 1, Required = true)]
		public double? UnderlyingSettlPrice { get; set; }
		
		[TagDetails(Tag = 733, Type = TagType.Int, Offset = 2, Required = true)]
		public int? UnderlyingSettlPriceType { get; set; }
		
	}
}
