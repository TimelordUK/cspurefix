using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class InstrumentExtension
	{
		[TagDetails(Tag = 668, Type = TagType.Int, Offset = 0)]
		public int? DeliveryForm { get; set; }
		
		[TagDetails(Tag = 869, Type = TagType.Float, Offset = 1)]
		public double? PctAtRisk { get; set; }
		
		[Component(Offset = 2)]
		public AttrbGrp? AttrbGrp { get; set; }
		
	}
}
