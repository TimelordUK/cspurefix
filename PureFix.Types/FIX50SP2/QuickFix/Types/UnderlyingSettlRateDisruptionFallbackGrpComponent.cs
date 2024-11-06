using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingSettlRateDisruptionFallbackGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40659, Offset = 0, Required = false)]
		public IOINoUnderlyingSettlRateFallbacks[]? NoUnderlyingSettlRateFallbacks {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingSettlRateFallbacks is not null && NoUnderlyingSettlRateFallbacks.Length != 0)
			{
				writer.WriteWholeNumber(40659, NoUnderlyingSettlRateFallbacks.Length);
				for (int i = 0; i < NoUnderlyingSettlRateFallbacks.Length; i++)
				{
					((IFixEncoder)NoUnderlyingSettlRateFallbacks[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingSettlRateFallbacks") is IMessageView viewNoUnderlyingSettlRateFallbacks)
			{
				var count = viewNoUnderlyingSettlRateFallbacks.GroupCount();
				NoUnderlyingSettlRateFallbacks = new IOINoUnderlyingSettlRateFallbacks[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingSettlRateFallbacks[i] = new();
					((IFixParser)NoUnderlyingSettlRateFallbacks[i]).Parse(viewNoUnderlyingSettlRateFallbacks.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingSettlRateFallbacks":
					value = NoUnderlyingSettlRateFallbacks;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingSettlRateFallbacks = null;
		}
	}
}
