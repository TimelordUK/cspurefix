using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingMarketDisruptionFallbackGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41866, Offset = 0, Required = false)]
		public IOINoUnderlyingMarketDisruptionFallbacks[]? NoUnderlyingMarketDisruptionFallbacks {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingMarketDisruptionFallbacks is not null && NoUnderlyingMarketDisruptionFallbacks.Length != 0)
			{
				writer.WriteWholeNumber(41866, NoUnderlyingMarketDisruptionFallbacks.Length);
				for (int i = 0; i < NoUnderlyingMarketDisruptionFallbacks.Length; i++)
				{
					((IFixEncoder)NoUnderlyingMarketDisruptionFallbacks[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingMarketDisruptionFallbacks") is IMessageView viewNoUnderlyingMarketDisruptionFallbacks)
			{
				var count = viewNoUnderlyingMarketDisruptionFallbacks.GroupCount();
				NoUnderlyingMarketDisruptionFallbacks = new IOINoUnderlyingMarketDisruptionFallbacks[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingMarketDisruptionFallbacks[i] = new();
					((IFixParser)NoUnderlyingMarketDisruptionFallbacks[i]).Parse(viewNoUnderlyingMarketDisruptionFallbacks.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingMarketDisruptionFallbacks":
					value = NoUnderlyingMarketDisruptionFallbacks;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingMarketDisruptionFallbacks = null;
		}
	}
}
