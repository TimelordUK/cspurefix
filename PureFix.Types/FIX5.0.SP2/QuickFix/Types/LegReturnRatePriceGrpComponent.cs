using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegReturnRatePriceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42564, Offset = 0, Required = false)]
		public NoLegReturnRatePrices[]? NoLegReturnRatePrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegReturnRatePrices is not null && NoLegReturnRatePrices.Length != 0)
			{
				writer.WriteWholeNumber(42564, NoLegReturnRatePrices.Length);
				for (int i = 0; i < NoLegReturnRatePrices.Length; i++)
				{
					((IFixEncoder)NoLegReturnRatePrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegReturnRatePrices") is IMessageView viewNoLegReturnRatePrices)
			{
				var count = viewNoLegReturnRatePrices.GroupCount();
				NoLegReturnRatePrices = new NoLegReturnRatePrices[count];
				for (int i = 0; i < count; i++)
				{
					NoLegReturnRatePrices[i] = new();
					((IFixParser)NoLegReturnRatePrices[i]).Parse(viewNoLegReturnRatePrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegReturnRatePrices":
					value = NoLegReturnRatePrices;
					break;
				default: return false;
			}
			return true;
		}
	}
}
