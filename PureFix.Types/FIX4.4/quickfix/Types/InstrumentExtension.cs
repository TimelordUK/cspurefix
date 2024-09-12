using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class InstrumentExtension
	{
		public int? DeliveryForm { get; set; } // 668 INT
		public double? PctAtRisk { get; set; } // 869 PERCENTAGE
		public AttrbGrp? AttrbGrp { get; set; }
	}
}
