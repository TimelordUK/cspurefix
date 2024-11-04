using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingSettlRateFallbackRateSourceComponent : IFixComponent
	{
		[TagDetails(Tag = 40904, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingSettlRateFallbackRateSource {get; set;}
		
		[TagDetails(Tag = 40915, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingSettlRateFallbackReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingSettlRateFallbackRateSource is not null) writer.WriteWholeNumber(40904, UnderlyingSettlRateFallbackRateSource.Value);
			if (UnderlyingSettlRateFallbackReferencePage is not null) writer.WriteString(40915, UnderlyingSettlRateFallbackReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingSettlRateFallbackRateSource = view.GetInt32(40904);
			UnderlyingSettlRateFallbackReferencePage = view.GetString(40915);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingSettlRateFallbackRateSource":
					value = UnderlyingSettlRateFallbackRateSource;
					break;
				case "UnderlyingSettlRateFallbackReferencePage":
					value = UnderlyingSettlRateFallbackReferencePage;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingSettlRateFallbackRateSource = null;
			UnderlyingSettlRateFallbackReferencePage = null;
		}
	}
}
