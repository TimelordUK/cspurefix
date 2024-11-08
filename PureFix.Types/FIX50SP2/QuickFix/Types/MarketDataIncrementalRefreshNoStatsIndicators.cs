using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarketDataIncrementalRefreshNoStatsIndicators : IFixGroup
	{
		[TagDetails(Tag = 1176, Type = TagType.Int, Offset = 0, Required = false)]
		public int? StatsType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StatsType is not null) writer.WriteWholeNumber(1176, StatsType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			StatsType = view.GetInt32(1176);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StatsType":
					value = StatsType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			StatsType = null;
		}
	}
}
