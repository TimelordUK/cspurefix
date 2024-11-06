using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40950, Offset = 0, Required = false)]
		public IOINoPaymentStreamFixingDateBusinessCenters[]? NoPaymentStreamFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamFixingDateBusinessCenters is not null && NoPaymentStreamFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40950, NoPaymentStreamFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStreamFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamFixingDateBusinessCenters") is IMessageView viewNoPaymentStreamFixingDateBusinessCenters)
			{
				var count = viewNoPaymentStreamFixingDateBusinessCenters.GroupCount();
				NoPaymentStreamFixingDateBusinessCenters = new IOINoPaymentStreamFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamFixingDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentStreamFixingDateBusinessCenters[i]).Parse(viewNoPaymentStreamFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamFixingDateBusinessCenters":
					value = NoPaymentStreamFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStreamFixingDateBusinessCenters = null;
		}
	}
}
