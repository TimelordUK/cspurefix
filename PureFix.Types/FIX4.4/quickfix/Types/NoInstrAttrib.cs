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
		[TagDetails(Tag = 871, Type = TagType.Int, Offset = 0, Required = false)]
		public int? InstrAttribType { get; set; }
		
		[TagDetails(Tag = 872, Type = TagType.String, Offset = 1, Required = false)]
		public string? InstrAttribValue { get; set; }
		
	}
}
