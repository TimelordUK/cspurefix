using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ReturnRateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42735, Offset = 0, Required = false)]
		public IOINoReturnRates[]? NoReturnRates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoReturnRates is not null && NoReturnRates.Length != 0)
			{
				writer.WriteWholeNumber(42735, NoReturnRates.Length);
				for (int i = 0; i < NoReturnRates.Length; i++)
				{
					((IFixEncoder)NoReturnRates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoReturnRates") is IMessageView viewNoReturnRates)
			{
				var count = viewNoReturnRates.GroupCount();
				NoReturnRates = new IOINoReturnRates[count];
				for (int i = 0; i < count; i++)
				{
					NoReturnRates[i] = new();
					((IFixParser)NoReturnRates[i]).Parse(viewNoReturnRates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoReturnRates":
					value = NoReturnRates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoReturnRates = null;
		}
	}
}
