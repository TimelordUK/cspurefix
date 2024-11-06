using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RelatedTradeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1855, Offset = 0, Required = false)]
		public MarketDataSnapshotFullRefreshNoRelatedTrades[]? NoRelatedTrades {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelatedTrades is not null && NoRelatedTrades.Length != 0)
			{
				writer.WriteWholeNumber(1855, NoRelatedTrades.Length);
				for (int i = 0; i < NoRelatedTrades.Length; i++)
				{
					((IFixEncoder)NoRelatedTrades[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelatedTrades") is IMessageView viewNoRelatedTrades)
			{
				var count = viewNoRelatedTrades.GroupCount();
				NoRelatedTrades = new MarketDataSnapshotFullRefreshNoRelatedTrades[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedTrades[i] = new();
					((IFixParser)NoRelatedTrades[i]).Parse(viewNoRelatedTrades.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedTrades":
					value = NoRelatedTrades;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRelatedTrades = null;
		}
	}
}
