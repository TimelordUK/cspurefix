using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityDefinitionNoMarketSegments : IFixGroup
	{
		[TagDetails(Tag = 1301, Type = TagType.String, Offset = 0, Required = false)]
		public string? MarketID {get; set;}
		
		[TagDetails(Tag = 1300, Type = TagType.String, Offset = 1, Required = false)]
		public string? MarketSegmentID {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public SecurityTradingRulesComponent? SecurityTradingRules {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public StrikeRulesComponent? StrikeRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MarketID is not null) writer.WriteString(1301, MarketID);
			if (MarketSegmentID is not null) writer.WriteString(1300, MarketSegmentID);
			if (SecurityTradingRules is not null) ((IFixEncoder)SecurityTradingRules).Encode(writer);
			if (StrikeRules is not null) ((IFixEncoder)StrikeRules).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MarketID = view.GetString(1301);
			MarketSegmentID = view.GetString(1300);
			if (view.GetView("SecurityTradingRules") is IMessageView viewSecurityTradingRules)
			{
				SecurityTradingRules = new();
				((IFixParser)SecurityTradingRules).Parse(viewSecurityTradingRules);
			}
			if (view.GetView("StrikeRules") is IMessageView viewStrikeRules)
			{
				StrikeRules = new();
				((IFixParser)StrikeRules).Parse(viewStrikeRules);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MarketID":
					value = MarketID;
					break;
				case "MarketSegmentID":
					value = MarketSegmentID;
					break;
				case "SecurityTradingRules":
					value = SecurityTradingRules;
					break;
				case "StrikeRules":
					value = StrikeRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MarketID = null;
			MarketSegmentID = null;
			((IFixReset?)SecurityTradingRules)?.Reset();
			((IFixReset?)StrikeRules)?.Reset();
		}
	}
}
