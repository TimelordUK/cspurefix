using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarketDisruptionFallbackGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41094, Offset = 0, Required = false)]
		public NoMarketDisruptionFallbacks[]? NoMarketDisruptionFallbacks {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMarketDisruptionFallbacks is not null && NoMarketDisruptionFallbacks.Length != 0)
			{
				writer.WriteWholeNumber(41094, NoMarketDisruptionFallbacks.Length);
				for (int i = 0; i < NoMarketDisruptionFallbacks.Length; i++)
				{
					((IFixEncoder)NoMarketDisruptionFallbacks[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMarketDisruptionFallbacks") is IMessageView viewNoMarketDisruptionFallbacks)
			{
				var count = viewNoMarketDisruptionFallbacks.GroupCount();
				NoMarketDisruptionFallbacks = new NoMarketDisruptionFallbacks[count];
				for (int i = 0; i < count; i++)
				{
					NoMarketDisruptionFallbacks[i] = new();
					((IFixParser)NoMarketDisruptionFallbacks[i]).Parse(viewNoMarketDisruptionFallbacks.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMarketDisruptionFallbacks":
					value = NoMarketDisruptionFallbacks;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMarketDisruptionFallbacks = null;
		}
	}
}
