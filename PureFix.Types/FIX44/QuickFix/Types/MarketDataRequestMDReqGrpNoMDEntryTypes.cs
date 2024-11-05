using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MarketDataRequestMDReqGrpNoMDEntryTypes : IFixGroup
	{
		[TagDetails(Tag = 269, Type = TagType.String, Offset = 0, Required = true)]
		public string? MDEntryType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				MDEntryType is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MDEntryType is not null) writer.WriteString(269, MDEntryType);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MDEntryType = view.GetString(269);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MDEntryType":
					value = MDEntryType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MDEntryType = null;
		}
	}
}
