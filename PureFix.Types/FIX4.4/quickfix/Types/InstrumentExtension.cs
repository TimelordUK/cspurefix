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
		[TagDetails(668)]
		public int? DeliveryForm { get; set; } // INT
		
		[TagDetails(869)]
		public double? PctAtRisk { get; set; } // PERCENTAGE
		
		public AttrbGrp? AttrbGrp { get; set; }
	}
}
