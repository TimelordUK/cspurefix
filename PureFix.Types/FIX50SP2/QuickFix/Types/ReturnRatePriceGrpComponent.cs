using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ReturnRatePriceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42765, Offset = 0, Required = false)]
		public IOINoReturnRatePrices[]? NoReturnRatePrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoReturnRatePrices is not null && NoReturnRatePrices.Length != 0)
			{
				writer.WriteWholeNumber(42765, NoReturnRatePrices.Length);
				for (int i = 0; i < NoReturnRatePrices.Length; i++)
				{
					((IFixEncoder)NoReturnRatePrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoReturnRatePrices") is IMessageView viewNoReturnRatePrices)
			{
				var count = viewNoReturnRatePrices.GroupCount();
				NoReturnRatePrices = new IOINoReturnRatePrices[count];
				for (int i = 0; i < count; i++)
				{
					NoReturnRatePrices[i] = new();
					((IFixParser)NoReturnRatePrices[i]).Parse(viewNoReturnRatePrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoReturnRatePrices":
					value = NoReturnRatePrices;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoReturnRatePrices = null;
		}
	}
}
