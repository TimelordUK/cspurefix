using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegReturnRateFXConversionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42530, Offset = 0, Required = false)]
		public NoLegReturnRateFXConversions[]? NoLegReturnRateFXConversions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegReturnRateFXConversions is not null && NoLegReturnRateFXConversions.Length != 0)
			{
				writer.WriteWholeNumber(42530, NoLegReturnRateFXConversions.Length);
				for (int i = 0; i < NoLegReturnRateFXConversions.Length; i++)
				{
					((IFixEncoder)NoLegReturnRateFXConversions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegReturnRateFXConversions") is IMessageView viewNoLegReturnRateFXConversions)
			{
				var count = viewNoLegReturnRateFXConversions.GroupCount();
				NoLegReturnRateFXConversions = new NoLegReturnRateFXConversions[count];
				for (int i = 0; i < count; i++)
				{
					NoLegReturnRateFXConversions[i] = new();
					((IFixParser)NoLegReturnRateFXConversions[i]).Parse(viewNoLegReturnRateFXConversions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegReturnRateFXConversions":
					value = NoLegReturnRateFXConversions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
