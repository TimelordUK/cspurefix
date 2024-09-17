using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SettlRateDisruptionFallbackGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40085, Offset = 0, Required = false)]
		public NoSettlRateFallbacks[]? NoSettlRateFallbacks {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlRateFallbacks is not null && NoSettlRateFallbacks.Length != 0)
			{
				writer.WriteWholeNumber(40085, NoSettlRateFallbacks.Length);
				for (int i = 0; i < NoSettlRateFallbacks.Length; i++)
				{
					((IFixEncoder)NoSettlRateFallbacks[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlRateFallbacks") is IMessageView viewNoSettlRateFallbacks)
			{
				var count = viewNoSettlRateFallbacks.GroupCount();
				NoSettlRateFallbacks = new NoSettlRateFallbacks[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlRateFallbacks[i] = new();
					((IFixParser)NoSettlRateFallbacks[i]).Parse(viewNoSettlRateFallbacks.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlRateFallbacks":
					value = NoSettlRateFallbacks;
					break;
				default: return false;
			}
			return true;
		}
	}
}
