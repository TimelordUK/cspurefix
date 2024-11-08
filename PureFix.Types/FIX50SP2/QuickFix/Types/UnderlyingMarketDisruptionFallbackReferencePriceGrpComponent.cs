using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingMarketDisruptionFallbackReferencePriceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41868, Offset = 0, Required = false)]
		public IOINoUnderlyingMarketDisruptionFallbackReferencePrices[]? NoUnderlyingMarketDisruptionFallbackReferencePrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingMarketDisruptionFallbackReferencePrices is not null && NoUnderlyingMarketDisruptionFallbackReferencePrices.Length != 0)
			{
				writer.WriteWholeNumber(41868, NoUnderlyingMarketDisruptionFallbackReferencePrices.Length);
				for (int i = 0; i < NoUnderlyingMarketDisruptionFallbackReferencePrices.Length; i++)
				{
					((IFixEncoder)NoUnderlyingMarketDisruptionFallbackReferencePrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingMarketDisruptionFallbackReferencePrices") is IMessageView viewNoUnderlyingMarketDisruptionFallbackReferencePrices)
			{
				var count = viewNoUnderlyingMarketDisruptionFallbackReferencePrices.GroupCount();
				NoUnderlyingMarketDisruptionFallbackReferencePrices = new IOINoUnderlyingMarketDisruptionFallbackReferencePrices[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingMarketDisruptionFallbackReferencePrices[i] = new();
					((IFixParser)NoUnderlyingMarketDisruptionFallbackReferencePrices[i]).Parse(viewNoUnderlyingMarketDisruptionFallbackReferencePrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingMarketDisruptionFallbackReferencePrices":
					value = NoUnderlyingMarketDisruptionFallbackReferencePrices;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingMarketDisruptionFallbackReferencePrices = null;
		}
	}
}
