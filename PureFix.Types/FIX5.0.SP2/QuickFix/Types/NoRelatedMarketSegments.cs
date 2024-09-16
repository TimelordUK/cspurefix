using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoRelatedMarketSegments : IFixGroup
	{
		[TagDetails(Tag = 2546, Type = TagType.String, Offset = 0, Required = false)]
		public string? RelatedMarketSegmentID {get; set;}
		
		[TagDetails(Tag = 2547, Type = TagType.Int, Offset = 1, Required = false)]
		public int? MarketSegmentRelationship {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RelatedMarketSegmentID is not null) writer.WriteString(2546, RelatedMarketSegmentID);
			if (MarketSegmentRelationship is not null) writer.WriteWholeNumber(2547, MarketSegmentRelationship.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RelatedMarketSegmentID = view.GetString(2546);
			MarketSegmentRelationship = view.GetInt32(2547);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RelatedMarketSegmentID":
					value = RelatedMarketSegmentID;
					break;
				case "MarketSegmentRelationship":
					value = MarketSegmentRelationship;
					break;
				default: return false;
			}
			return true;
		}
	}
}
