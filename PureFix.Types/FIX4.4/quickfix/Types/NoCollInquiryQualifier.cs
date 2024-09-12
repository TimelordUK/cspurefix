using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoCollInquiryQualifier
	{
		[TagDetails(Tag = 896, Type = TagType.Int, Offset = 0)]
		public int? CollInquiryQualifier { get; set; }
		
	}
}
