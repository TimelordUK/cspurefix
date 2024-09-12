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
		[TagDetails(668, TagType.Int)]
		public int? DeliveryForm { get; set; }
		
		[TagDetails(869, TagType.Float)]
		public double? PctAtRisk { get; set; }
		
		[Component]
		public AttrbGrp? AttrbGrp { get; set; }
		
	}
}
