using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegReturnRateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42534, Offset = 0, Required = false)]
		public NoLegReturnRates[]? NoLegReturnRates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegReturnRates is not null && NoLegReturnRates.Length != 0)
			{
				writer.WriteWholeNumber(42534, NoLegReturnRates.Length);
				for (int i = 0; i < NoLegReturnRates.Length; i++)
				{
					((IFixEncoder)NoLegReturnRates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegReturnRates") is IMessageView viewNoLegReturnRates)
			{
				var count = viewNoLegReturnRates.GroupCount();
				NoLegReturnRates = new NoLegReturnRates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegReturnRates[i] = new();
					((IFixParser)NoLegReturnRates[i]).Parse(viewNoLegReturnRates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegReturnRates":
					value = NoLegReturnRates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
