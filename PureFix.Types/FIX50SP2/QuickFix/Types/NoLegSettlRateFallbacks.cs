using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegSettlRateFallbacks : IFixGroup
	{
		[TagDetails(Tag = 40903, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegSettlRatePostponementMaximumDays {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public LegSettlRateFallbackRateSourceComponent? LegSettlRateFallbackRateSource {get; set;}
		
		[TagDetails(Tag = 40905, Type = TagType.Boolean, Offset = 2, Required = false)]
		public bool? LegSettlRatePostponementSurvey {get; set;}
		
		[TagDetails(Tag = 40906, Type = TagType.Int, Offset = 3, Required = false)]
		public int? LegSettlRatePostponementCalculationAgent {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegSettlRatePostponementMaximumDays is not null) writer.WriteWholeNumber(40903, LegSettlRatePostponementMaximumDays.Value);
			if (LegSettlRateFallbackRateSource is not null) ((IFixEncoder)LegSettlRateFallbackRateSource).Encode(writer);
			if (LegSettlRatePostponementSurvey is not null) writer.WriteBoolean(40905, LegSettlRatePostponementSurvey.Value);
			if (LegSettlRatePostponementCalculationAgent is not null) writer.WriteWholeNumber(40906, LegSettlRatePostponementCalculationAgent.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegSettlRatePostponementMaximumDays = view.GetInt32(40903);
			if (view.GetView("LegSettlRateFallbackRateSource") is IMessageView viewLegSettlRateFallbackRateSource)
			{
				LegSettlRateFallbackRateSource = new();
				((IFixParser)LegSettlRateFallbackRateSource).Parse(viewLegSettlRateFallbackRateSource);
			}
			LegSettlRatePostponementSurvey = view.GetBool(40905);
			LegSettlRatePostponementCalculationAgent = view.GetInt32(40906);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegSettlRatePostponementMaximumDays":
					value = LegSettlRatePostponementMaximumDays;
					break;
				case "LegSettlRateFallbackRateSource":
					value = LegSettlRateFallbackRateSource;
					break;
				case "LegSettlRatePostponementSurvey":
					value = LegSettlRatePostponementSurvey;
					break;
				case "LegSettlRatePostponementCalculationAgent":
					value = LegSettlRatePostponementCalculationAgent;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegSettlRatePostponementMaximumDays = null;
			((IFixReset?)LegSettlRateFallbackRateSource)?.Reset();
			LegSettlRatePostponementSurvey = null;
			LegSettlRatePostponementCalculationAgent = null;
		}
	}
}
