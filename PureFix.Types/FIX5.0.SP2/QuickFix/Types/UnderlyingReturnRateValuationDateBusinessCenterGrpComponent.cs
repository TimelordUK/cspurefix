using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingReturnRateValuationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43069, Offset = 0, Required = false)]
		public NoUnderlyingReturnRateValuationDateBusinessCenters[]? NoUnderlyingReturnRateValuationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingReturnRateValuationDateBusinessCenters is not null && NoUnderlyingReturnRateValuationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(43069, NoUnderlyingReturnRateValuationDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingReturnRateValuationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingReturnRateValuationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingReturnRateValuationDateBusinessCenters") is IMessageView viewNoUnderlyingReturnRateValuationDateBusinessCenters)
			{
				var count = viewNoUnderlyingReturnRateValuationDateBusinessCenters.GroupCount();
				NoUnderlyingReturnRateValuationDateBusinessCenters = new NoUnderlyingReturnRateValuationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingReturnRateValuationDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingReturnRateValuationDateBusinessCenters[i]).Parse(viewNoUnderlyingReturnRateValuationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingReturnRateValuationDateBusinessCenters":
					value = NoUnderlyingReturnRateValuationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingReturnRateValuationDateBusinessCenters = null;
		}
	}
}
