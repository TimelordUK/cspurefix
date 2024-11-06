using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ReturnRateFXConversionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42731, Offset = 0, Required = false)]
		public IOINoReturnRateFXConversions[]? NoReturnRateFXConversions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoReturnRateFXConversions is not null && NoReturnRateFXConversions.Length != 0)
			{
				writer.WriteWholeNumber(42731, NoReturnRateFXConversions.Length);
				for (int i = 0; i < NoReturnRateFXConversions.Length; i++)
				{
					((IFixEncoder)NoReturnRateFXConversions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoReturnRateFXConversions") is IMessageView viewNoReturnRateFXConversions)
			{
				var count = viewNoReturnRateFXConversions.GroupCount();
				NoReturnRateFXConversions = new IOINoReturnRateFXConversions[count];
				for (int i = 0; i < count; i++)
				{
					NoReturnRateFXConversions[i] = new();
					((IFixParser)NoReturnRateFXConversions[i]).Parse(viewNoReturnRateFXConversions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoReturnRateFXConversions":
					value = NoReturnRateFXConversions;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoReturnRateFXConversions = null;
		}
	}
}
