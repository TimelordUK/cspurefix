using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingReturnRatePriceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43064, Offset = 0, Required = false)]
		public IOINoUnderlyingReturnRatePrices[]? NoUnderlyingReturnRatePrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingReturnRatePrices is not null && NoUnderlyingReturnRatePrices.Length != 0)
			{
				writer.WriteWholeNumber(43064, NoUnderlyingReturnRatePrices.Length);
				for (int i = 0; i < NoUnderlyingReturnRatePrices.Length; i++)
				{
					((IFixEncoder)NoUnderlyingReturnRatePrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingReturnRatePrices") is IMessageView viewNoUnderlyingReturnRatePrices)
			{
				var count = viewNoUnderlyingReturnRatePrices.GroupCount();
				NoUnderlyingReturnRatePrices = new IOINoUnderlyingReturnRatePrices[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingReturnRatePrices[i] = new();
					((IFixParser)NoUnderlyingReturnRatePrices[i]).Parse(viewNoUnderlyingReturnRatePrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingReturnRatePrices":
					value = NoUnderlyingReturnRatePrices;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingReturnRatePrices = null;
		}
	}
}
