using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StatsIndGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1175, Offset = 0, Required = false)]
		public MarketDataIncrementalRefreshNoStatsIndicators[]? NoStatsIndicators {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStatsIndicators is not null && NoStatsIndicators.Length != 0)
			{
				writer.WriteWholeNumber(1175, NoStatsIndicators.Length);
				for (int i = 0; i < NoStatsIndicators.Length; i++)
				{
					((IFixEncoder)NoStatsIndicators[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStatsIndicators") is IMessageView viewNoStatsIndicators)
			{
				var count = viewNoStatsIndicators.GroupCount();
				NoStatsIndicators = new MarketDataIncrementalRefreshNoStatsIndicators[count];
				for (int i = 0; i < count; i++)
				{
					NoStatsIndicators[i] = new();
					((IFixParser)NoStatsIndicators[i]).Parse(viewNoStatsIndicators.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStatsIndicators":
					value = NoStatsIndicators;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStatsIndicators = null;
		}
	}
}
