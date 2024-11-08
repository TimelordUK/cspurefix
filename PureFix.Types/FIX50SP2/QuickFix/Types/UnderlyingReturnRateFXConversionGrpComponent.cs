using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingReturnRateFXConversionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43030, Offset = 0, Required = false)]
		public IOINoUnderlyingReturnRateFXConversions[]? NoUnderlyingReturnRateFXConversions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingReturnRateFXConversions is not null && NoUnderlyingReturnRateFXConversions.Length != 0)
			{
				writer.WriteWholeNumber(43030, NoUnderlyingReturnRateFXConversions.Length);
				for (int i = 0; i < NoUnderlyingReturnRateFXConversions.Length; i++)
				{
					((IFixEncoder)NoUnderlyingReturnRateFXConversions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingReturnRateFXConversions") is IMessageView viewNoUnderlyingReturnRateFXConversions)
			{
				var count = viewNoUnderlyingReturnRateFXConversions.GroupCount();
				NoUnderlyingReturnRateFXConversions = new IOINoUnderlyingReturnRateFXConversions[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingReturnRateFXConversions[i] = new();
					((IFixParser)NoUnderlyingReturnRateFXConversions[i]).Parse(viewNoUnderlyingReturnRateFXConversions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingReturnRateFXConversions":
					value = NoUnderlyingReturnRateFXConversions;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingReturnRateFXConversions = null;
		}
	}
}
