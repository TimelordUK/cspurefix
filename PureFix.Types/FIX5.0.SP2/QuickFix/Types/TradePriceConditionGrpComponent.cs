using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradePriceConditionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1838, Offset = 0, Required = false)]
		public NoTradePriceConditions[]? NoTradePriceConditions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTradePriceConditions is not null && NoTradePriceConditions.Length != 0)
			{
				writer.WriteWholeNumber(1838, NoTradePriceConditions.Length);
				for (int i = 0; i < NoTradePriceConditions.Length; i++)
				{
					((IFixEncoder)NoTradePriceConditions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTradePriceConditions") is IMessageView viewNoTradePriceConditions)
			{
				var count = viewNoTradePriceConditions.GroupCount();
				NoTradePriceConditions = new NoTradePriceConditions[count];
				for (int i = 0; i < count; i++)
				{
					NoTradePriceConditions[i] = new();
					((IFixParser)NoTradePriceConditions[i]).Parse(viewNoTradePriceConditions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTradePriceConditions":
					value = NoTradePriceConditions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
