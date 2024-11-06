using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingReturnRateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43034, Offset = 0, Required = false)]
		public IOINoUnderlyingReturnRates[]? NoUnderlyingReturnRates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingReturnRates is not null && NoUnderlyingReturnRates.Length != 0)
			{
				writer.WriteWholeNumber(43034, NoUnderlyingReturnRates.Length);
				for (int i = 0; i < NoUnderlyingReturnRates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingReturnRates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingReturnRates") is IMessageView viewNoUnderlyingReturnRates)
			{
				var count = viewNoUnderlyingReturnRates.GroupCount();
				NoUnderlyingReturnRates = new IOINoUnderlyingReturnRates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingReturnRates[i] = new();
					((IFixParser)NoUnderlyingReturnRates[i]).Parse(viewNoUnderlyingReturnRates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingReturnRates":
					value = NoUnderlyingReturnRates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingReturnRates = null;
		}
	}
}
