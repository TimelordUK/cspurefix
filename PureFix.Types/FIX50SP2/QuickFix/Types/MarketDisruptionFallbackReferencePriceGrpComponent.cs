using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarketDisruptionFallbackReferencePriceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41096, Offset = 0, Required = false)]
		public IOINoMarketDisruptionFallbackReferencePrices[]? NoMarketDisruptionFallbackReferencePrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMarketDisruptionFallbackReferencePrices is not null && NoMarketDisruptionFallbackReferencePrices.Length != 0)
			{
				writer.WriteWholeNumber(41096, NoMarketDisruptionFallbackReferencePrices.Length);
				for (int i = 0; i < NoMarketDisruptionFallbackReferencePrices.Length; i++)
				{
					((IFixEncoder)NoMarketDisruptionFallbackReferencePrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMarketDisruptionFallbackReferencePrices") is IMessageView viewNoMarketDisruptionFallbackReferencePrices)
			{
				var count = viewNoMarketDisruptionFallbackReferencePrices.GroupCount();
				NoMarketDisruptionFallbackReferencePrices = new IOINoMarketDisruptionFallbackReferencePrices[count];
				for (int i = 0; i < count; i++)
				{
					NoMarketDisruptionFallbackReferencePrices[i] = new();
					((IFixParser)NoMarketDisruptionFallbackReferencePrices[i]).Parse(viewNoMarketDisruptionFallbackReferencePrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMarketDisruptionFallbackReferencePrices":
					value = NoMarketDisruptionFallbackReferencePrices;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMarketDisruptionFallbackReferencePrices = null;
		}
	}
}
