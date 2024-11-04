using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoSettlRateFallbacks : IFixGroup
	{
		[TagDetails(Tag = 40086, Type = TagType.Int, Offset = 0, Required = false)]
		public int? SettlRatePostponementMaximumDays {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public SettlRateFallbackRateSourceComponent? SettlRateFallbackRateSource {get; set;}
		
		[TagDetails(Tag = 40088, Type = TagType.Boolean, Offset = 2, Required = false)]
		public bool? SettlRatePostponementSurvey {get; set;}
		
		[TagDetails(Tag = 40089, Type = TagType.Int, Offset = 3, Required = false)]
		public int? SettlRatePostponementCalculationAgent {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SettlRatePostponementMaximumDays is not null) writer.WriteWholeNumber(40086, SettlRatePostponementMaximumDays.Value);
			if (SettlRateFallbackRateSource is not null) ((IFixEncoder)SettlRateFallbackRateSource).Encode(writer);
			if (SettlRatePostponementSurvey is not null) writer.WriteBoolean(40088, SettlRatePostponementSurvey.Value);
			if (SettlRatePostponementCalculationAgent is not null) writer.WriteWholeNumber(40089, SettlRatePostponementCalculationAgent.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			SettlRatePostponementMaximumDays = view.GetInt32(40086);
			if (view.GetView("SettlRateFallbackRateSource") is IMessageView viewSettlRateFallbackRateSource)
			{
				SettlRateFallbackRateSource = new();
				((IFixParser)SettlRateFallbackRateSource).Parse(viewSettlRateFallbackRateSource);
			}
			SettlRatePostponementSurvey = view.GetBool(40088);
			SettlRatePostponementCalculationAgent = view.GetInt32(40089);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "SettlRatePostponementMaximumDays":
					value = SettlRatePostponementMaximumDays;
					break;
				case "SettlRateFallbackRateSource":
					value = SettlRateFallbackRateSource;
					break;
				case "SettlRatePostponementSurvey":
					value = SettlRatePostponementSurvey;
					break;
				case "SettlRatePostponementCalculationAgent":
					value = SettlRatePostponementCalculationAgent;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			SettlRatePostponementMaximumDays = null;
			((IFixReset?)SettlRateFallbackRateSource)?.Reset();
			SettlRatePostponementSurvey = null;
			SettlRatePostponementCalculationAgent = null;
		}
	}
}
