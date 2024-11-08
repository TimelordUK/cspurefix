using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegMarketDisruptionFallbackGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41469, Offset = 0, Required = false)]
		public IOINoLegMarketDisruptionFallbacks[]? NoLegMarketDisruptionFallbacks {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegMarketDisruptionFallbacks is not null && NoLegMarketDisruptionFallbacks.Length != 0)
			{
				writer.WriteWholeNumber(41469, NoLegMarketDisruptionFallbacks.Length);
				for (int i = 0; i < NoLegMarketDisruptionFallbacks.Length; i++)
				{
					((IFixEncoder)NoLegMarketDisruptionFallbacks[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegMarketDisruptionFallbacks") is IMessageView viewNoLegMarketDisruptionFallbacks)
			{
				var count = viewNoLegMarketDisruptionFallbacks.GroupCount();
				NoLegMarketDisruptionFallbacks = new IOINoLegMarketDisruptionFallbacks[count];
				for (int i = 0; i < count; i++)
				{
					NoLegMarketDisruptionFallbacks[i] = new();
					((IFixParser)NoLegMarketDisruptionFallbacks[i]).Parse(viewNoLegMarketDisruptionFallbacks.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegMarketDisruptionFallbacks":
					value = NoLegMarketDisruptionFallbacks;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegMarketDisruptionFallbacks = null;
		}
	}
}
