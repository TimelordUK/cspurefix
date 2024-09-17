using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarketSegmentScopeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1310, Offset = 0, Required = false)]
		public NoMarketSegments[]? NoMarketSegments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMarketSegments is not null && NoMarketSegments.Length != 0)
			{
				writer.WriteWholeNumber(1310, NoMarketSegments.Length);
				for (int i = 0; i < NoMarketSegments.Length; i++)
				{
					((IFixEncoder)NoMarketSegments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMarketSegments") is IMessageView viewNoMarketSegments)
			{
				var count = viewNoMarketSegments.GroupCount();
				NoMarketSegments = new NoMarketSegments[count];
				for (int i = 0; i < count; i++)
				{
					NoMarketSegments[i] = new();
					((IFixParser)NoMarketSegments[i]).Parse(viewNoMarketSegments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMarketSegments":
					value = NoMarketSegments;
					break;
				default: return false;
			}
			return true;
		}
	}
}
