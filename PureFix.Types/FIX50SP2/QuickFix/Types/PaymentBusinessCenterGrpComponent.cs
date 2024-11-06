using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40944, Offset = 0, Required = false)]
		public ExecutionReportNoPaymentBusinessCenters[]? NoPaymentBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentBusinessCenters is not null && NoPaymentBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40944, NoPaymentBusinessCenters.Length);
				for (int i = 0; i < NoPaymentBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentBusinessCenters") is IMessageView viewNoPaymentBusinessCenters)
			{
				var count = viewNoPaymentBusinessCenters.GroupCount();
				NoPaymentBusinessCenters = new ExecutionReportNoPaymentBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentBusinessCenters[i] = new();
					((IFixParser)NoPaymentBusinessCenters[i]).Parse(viewNoPaymentBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentBusinessCenters":
					value = NoPaymentBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentBusinessCenters = null;
		}
	}
}
