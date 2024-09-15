using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MDReqGrpNoMDEntryTypes : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 269, Type = TagType.String, Offset = 0, Required = true)]
		public string? MDEntryType { get; set; }
		
	}
}
