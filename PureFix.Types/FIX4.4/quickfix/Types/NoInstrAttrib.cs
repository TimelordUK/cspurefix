using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoInstrAttrib
	{
		[TagDetails(871, TagType.Int)]
		public int? InstrAttribType { get; set; }
		
		[TagDetails(872, TagType.String)]
		public string? InstrAttribValue { get; set; }
		
	}
}
