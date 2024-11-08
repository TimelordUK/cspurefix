using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OrderMassActionReportNoAffectedMarketSegments : IFixGroup
	{
		[TagDetails(Tag = 1792, Type = TagType.String, Offset = 0, Required = false)]
		public string? AffectedMarketSegmentID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AffectedMarketSegmentID is not null) writer.WriteString(1792, AffectedMarketSegmentID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AffectedMarketSegmentID = view.GetString(1792);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "AffectedMarketSegmentID":
					value = AffectedMarketSegmentID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			AffectedMarketSegmentID = null;
		}
	}
}
