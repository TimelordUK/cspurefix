using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingSettlRateFallbacks : IFixGroup
	{
		[TagDetails(Tag = 40660, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingSettlRatePostponementMaximumDays {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public UnderlyingSettlRateFallbackRateSourceComponent? UnderlyingSettlRateFallbackRateSource {get; set;}
		
		[TagDetails(Tag = 40662, Type = TagType.Boolean, Offset = 2, Required = false)]
		public bool? UnderlyingSettlRatePostponementSurvey {get; set;}
		
		[TagDetails(Tag = 40663, Type = TagType.Int, Offset = 3, Required = false)]
		public int? UnderlyingSettlRatePostponementCalculationAgent {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingSettlRatePostponementMaximumDays is not null) writer.WriteWholeNumber(40660, UnderlyingSettlRatePostponementMaximumDays.Value);
			if (UnderlyingSettlRateFallbackRateSource is not null) ((IFixEncoder)UnderlyingSettlRateFallbackRateSource).Encode(writer);
			if (UnderlyingSettlRatePostponementSurvey is not null) writer.WriteBoolean(40662, UnderlyingSettlRatePostponementSurvey.Value);
			if (UnderlyingSettlRatePostponementCalculationAgent is not null) writer.WriteWholeNumber(40663, UnderlyingSettlRatePostponementCalculationAgent.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingSettlRatePostponementMaximumDays = view.GetInt32(40660);
			if (view.GetView("UnderlyingSettlRateFallbackRateSource") is IMessageView viewUnderlyingSettlRateFallbackRateSource)
			{
				UnderlyingSettlRateFallbackRateSource = new();
				((IFixParser)UnderlyingSettlRateFallbackRateSource).Parse(viewUnderlyingSettlRateFallbackRateSource);
			}
			UnderlyingSettlRatePostponementSurvey = view.GetBool(40662);
			UnderlyingSettlRatePostponementCalculationAgent = view.GetInt32(40663);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingSettlRatePostponementMaximumDays":
					value = UnderlyingSettlRatePostponementMaximumDays;
					break;
				case "UnderlyingSettlRateFallbackRateSource":
					value = UnderlyingSettlRateFallbackRateSource;
					break;
				case "UnderlyingSettlRatePostponementSurvey":
					value = UnderlyingSettlRatePostponementSurvey;
					break;
				case "UnderlyingSettlRatePostponementCalculationAgent":
					value = UnderlyingSettlRatePostponementCalculationAgent;
					break;
				default: return false;
			}
			return true;
		}
	}
}
