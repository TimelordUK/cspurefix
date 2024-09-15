using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrumentExtension : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 668, Type = TagType.Int, Offset = 0, Required = false)]
		public int? DeliveryForm { get; set; }
		
		[TagDetails(Tag = 869, Type = TagType.Float, Offset = 1, Required = false)]
		public double? PctAtRisk { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public AttrbGrp? AttrbGrp { get; set; }
		
	}
}
