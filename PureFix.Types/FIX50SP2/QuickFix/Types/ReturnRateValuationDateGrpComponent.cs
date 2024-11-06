using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ReturnRateValuationDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42772, Offset = 0, Required = false)]
		public IOINoReturnRateValuationDates[]? NoReturnRateValuationDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoReturnRateValuationDates is not null && NoReturnRateValuationDates.Length != 0)
			{
				writer.WriteWholeNumber(42772, NoReturnRateValuationDates.Length);
				for (int i = 0; i < NoReturnRateValuationDates.Length; i++)
				{
					((IFixEncoder)NoReturnRateValuationDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoReturnRateValuationDates") is IMessageView viewNoReturnRateValuationDates)
			{
				var count = viewNoReturnRateValuationDates.GroupCount();
				NoReturnRateValuationDates = new IOINoReturnRateValuationDates[count];
				for (int i = 0; i < count; i++)
				{
					NoReturnRateValuationDates[i] = new();
					((IFixParser)NoReturnRateValuationDates[i]).Parse(viewNoReturnRateValuationDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoReturnRateValuationDates":
					value = NoReturnRateValuationDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoReturnRateValuationDates = null;
		}
	}
}
