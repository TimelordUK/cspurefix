using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoEvents
	{
		[TagDetails(865, TagType.Int)]
		public int? EventType { get; set; }
		
		[TagDetails(866, TagType.LocalDate)]
		public DateTime? EventDate { get; set; }
		
		[TagDetails(867, TagType.Float)]
		public double? EventPx { get; set; }
		
		[TagDetails(868, TagType.String)]
		public string? EventText { get; set; }
		
	}
}
