using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CollInqQualGrpNoCollInquiryQualifier : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 896, Type = TagType.Int, Offset = 0, Required = false)]
		public int? CollInquiryQualifier { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (CollInquiryQualifier is not null) writer.WriteWholeNumber(896, CollInquiryQualifier.Value);
		}
	}
}
