using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingReturnRateValuationDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43071, Offset = 0, Required = false)]
		public IOINoUnderlyingReturnRateValuationDates[]? NoUnderlyingReturnRateValuationDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingReturnRateValuationDates is not null && NoUnderlyingReturnRateValuationDates.Length != 0)
			{
				writer.WriteWholeNumber(43071, NoUnderlyingReturnRateValuationDates.Length);
				for (int i = 0; i < NoUnderlyingReturnRateValuationDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingReturnRateValuationDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingReturnRateValuationDates") is IMessageView viewNoUnderlyingReturnRateValuationDates)
			{
				var count = viewNoUnderlyingReturnRateValuationDates.GroupCount();
				NoUnderlyingReturnRateValuationDates = new IOINoUnderlyingReturnRateValuationDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingReturnRateValuationDates[i] = new();
					((IFixParser)NoUnderlyingReturnRateValuationDates[i]).Parse(viewNoUnderlyingReturnRateValuationDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingReturnRateValuationDates":
					value = NoUnderlyingReturnRateValuationDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingReturnRateValuationDates = null;
		}
	}
}
