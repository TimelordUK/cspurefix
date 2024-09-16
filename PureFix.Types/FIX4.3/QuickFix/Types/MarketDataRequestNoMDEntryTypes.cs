using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class MarketDataRequestNoMDEntryTypes : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 269, Type = TagType.String, Offset = 0, Required = true)]
		public string? MDEntryType { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				MDEntryType is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MDEntryType is not null) writer.WriteString(269, MDEntryType);
		}
	}
}
