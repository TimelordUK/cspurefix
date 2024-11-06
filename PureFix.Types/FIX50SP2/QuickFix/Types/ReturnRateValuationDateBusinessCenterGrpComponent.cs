using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ReturnRateValuationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42770, Offset = 0, Required = false)]
		public IOINoReturnRateValuationDateBusinessCenters[]? NoReturnRateValuationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoReturnRateValuationDateBusinessCenters is not null && NoReturnRateValuationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42770, NoReturnRateValuationDateBusinessCenters.Length);
				for (int i = 0; i < NoReturnRateValuationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoReturnRateValuationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoReturnRateValuationDateBusinessCenters") is IMessageView viewNoReturnRateValuationDateBusinessCenters)
			{
				var count = viewNoReturnRateValuationDateBusinessCenters.GroupCount();
				NoReturnRateValuationDateBusinessCenters = new IOINoReturnRateValuationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoReturnRateValuationDateBusinessCenters[i] = new();
					((IFixParser)NoReturnRateValuationDateBusinessCenters[i]).Parse(viewNoReturnRateValuationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoReturnRateValuationDateBusinessCenters":
					value = NoReturnRateValuationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoReturnRateValuationDateBusinessCenters = null;
		}
	}
}
