using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OrderMassActionReportNoTargetMarketSegments : IFixGroup
	{
		[TagDetails(Tag = 1790, Type = TagType.String, Offset = 0, Required = false)]
		public string? TargetMarketSegmentID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TargetMarketSegmentID is not null) writer.WriteString(1790, TargetMarketSegmentID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TargetMarketSegmentID = view.GetString(1790);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TargetMarketSegmentID":
					value = TargetMarketSegmentID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TargetMarketSegmentID = null;
		}
	}
}
