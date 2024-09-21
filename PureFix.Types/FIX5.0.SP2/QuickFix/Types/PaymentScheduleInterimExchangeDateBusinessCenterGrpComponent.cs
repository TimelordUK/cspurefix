using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentScheduleInterimExchangeDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40945, Offset = 0, Required = false)]
		public NoPaymentScheduleInterimExchangeDateBusinessCenters[]? NoPaymentScheduleInterimExchangeDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentScheduleInterimExchangeDateBusinessCenters is not null && NoPaymentScheduleInterimExchangeDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40945, NoPaymentScheduleInterimExchangeDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentScheduleInterimExchangeDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentScheduleInterimExchangeDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentScheduleInterimExchangeDateBusinessCenters") is IMessageView viewNoPaymentScheduleInterimExchangeDateBusinessCenters)
			{
				var count = viewNoPaymentScheduleInterimExchangeDateBusinessCenters.GroupCount();
				NoPaymentScheduleInterimExchangeDateBusinessCenters = new NoPaymentScheduleInterimExchangeDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentScheduleInterimExchangeDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentScheduleInterimExchangeDateBusinessCenters[i]).Parse(viewNoPaymentScheduleInterimExchangeDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentScheduleInterimExchangeDateBusinessCenters":
					value = NoPaymentScheduleInterimExchangeDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentScheduleInterimExchangeDateBusinessCenters = null;
		}
	}
}
