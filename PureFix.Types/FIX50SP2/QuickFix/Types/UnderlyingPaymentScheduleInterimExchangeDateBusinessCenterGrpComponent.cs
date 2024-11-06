using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentScheduleInterimExchangeDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40967, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters[]? NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters is not null && NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40967, NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters") is IMessageView viewNoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters.GroupCount();
				NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters = new IOINoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters[i]).Parse(viewNoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters":
					value = NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentScheduleInterimExchangeDateBusinessCenters = null;
		}
	}
}
