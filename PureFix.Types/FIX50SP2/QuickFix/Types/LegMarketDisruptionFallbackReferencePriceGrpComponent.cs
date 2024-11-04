using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegMarketDisruptionFallbackReferencePriceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41471, Offset = 0, Required = false)]
		public NoLegMarketDisruptionFallbackReferencePrices[]? NoLegMarketDisruptionFallbackReferencePrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegMarketDisruptionFallbackReferencePrices is not null && NoLegMarketDisruptionFallbackReferencePrices.Length != 0)
			{
				writer.WriteWholeNumber(41471, NoLegMarketDisruptionFallbackReferencePrices.Length);
				for (int i = 0; i < NoLegMarketDisruptionFallbackReferencePrices.Length; i++)
				{
					((IFixEncoder)NoLegMarketDisruptionFallbackReferencePrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegMarketDisruptionFallbackReferencePrices") is IMessageView viewNoLegMarketDisruptionFallbackReferencePrices)
			{
				var count = viewNoLegMarketDisruptionFallbackReferencePrices.GroupCount();
				NoLegMarketDisruptionFallbackReferencePrices = new NoLegMarketDisruptionFallbackReferencePrices[count];
				for (int i = 0; i < count; i++)
				{
					NoLegMarketDisruptionFallbackReferencePrices[i] = new();
					((IFixParser)NoLegMarketDisruptionFallbackReferencePrices[i]).Parse(viewNoLegMarketDisruptionFallbackReferencePrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegMarketDisruptionFallbackReferencePrices":
					value = NoLegMarketDisruptionFallbackReferencePrices;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegMarketDisruptionFallbackReferencePrices = null;
		}
	}
}
