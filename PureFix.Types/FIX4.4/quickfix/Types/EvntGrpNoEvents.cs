using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class EvntGrpNoEvents
	{
		[TagDetails(Tag = 865, Type = TagType.Int, Offset = 0, Required = false)]
		public int? EventType { get; set; }
		
		[TagDetails(Tag = 866, Type = TagType.LocalDate, Offset = 1, Required = false)]
		public DateTime? EventDate { get; set; }
		
		[TagDetails(Tag = 867, Type = TagType.Float, Offset = 2, Required = false)]
		public double? EventPx { get; set; }
		
		[TagDetails(Tag = 868, Type = TagType.String, Offset = 3, Required = false)]
		public string? EventText { get; set; }
		
	}
}
