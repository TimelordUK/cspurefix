using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class QuotQualGrpNoQuoteQualifiers
	{
		[TagDetails(Tag = 695, Type = TagType.String, Offset = 0, Required = false)]
		public string? QuoteQualifier { get; set; }
		
	}
}
