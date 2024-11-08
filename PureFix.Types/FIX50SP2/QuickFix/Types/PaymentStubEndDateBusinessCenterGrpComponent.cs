using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStubEndDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42696, Offset = 0, Required = false)]
		public IOINoPaymentStubEndDateBusinessCenters[]? NoPaymentStubEndDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStubEndDateBusinessCenters is not null && NoPaymentStubEndDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42696, NoPaymentStubEndDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStubEndDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStubEndDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStubEndDateBusinessCenters") is IMessageView viewNoPaymentStubEndDateBusinessCenters)
			{
				var count = viewNoPaymentStubEndDateBusinessCenters.GroupCount();
				NoPaymentStubEndDateBusinessCenters = new IOINoPaymentStubEndDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStubEndDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentStubEndDateBusinessCenters[i]).Parse(viewNoPaymentStubEndDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStubEndDateBusinessCenters":
					value = NoPaymentStubEndDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStubEndDateBusinessCenters = null;
		}
	}
}
