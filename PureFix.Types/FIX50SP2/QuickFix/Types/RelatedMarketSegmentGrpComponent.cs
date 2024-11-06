using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RelatedMarketSegmentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2545, Offset = 0, Required = false)]
		public MarketDefinitionNoRelatedMarketSegments[]? NoRelatedMarketSegments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelatedMarketSegments is not null && NoRelatedMarketSegments.Length != 0)
			{
				writer.WriteWholeNumber(2545, NoRelatedMarketSegments.Length);
				for (int i = 0; i < NoRelatedMarketSegments.Length; i++)
				{
					((IFixEncoder)NoRelatedMarketSegments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelatedMarketSegments") is IMessageView viewNoRelatedMarketSegments)
			{
				var count = viewNoRelatedMarketSegments.GroupCount();
				NoRelatedMarketSegments = new MarketDefinitionNoRelatedMarketSegments[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedMarketSegments[i] = new();
					((IFixParser)NoRelatedMarketSegments[i]).Parse(viewNoRelatedMarketSegments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedMarketSegments":
					value = NoRelatedMarketSegments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRelatedMarketSegments = null;
		}
	}
}
