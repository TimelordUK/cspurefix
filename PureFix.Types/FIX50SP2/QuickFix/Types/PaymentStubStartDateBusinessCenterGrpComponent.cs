using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStubStartDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42705, Offset = 0, Required = false)]
		public IOINoPaymentStubStartDateBusinessCenters[]? NoPaymentStubStartDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStubStartDateBusinessCenters is not null && NoPaymentStubStartDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42705, NoPaymentStubStartDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStubStartDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStubStartDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStubStartDateBusinessCenters") is IMessageView viewNoPaymentStubStartDateBusinessCenters)
			{
				var count = viewNoPaymentStubStartDateBusinessCenters.GroupCount();
				NoPaymentStubStartDateBusinessCenters = new IOINoPaymentStubStartDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStubStartDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentStubStartDateBusinessCenters[i]).Parse(viewNoPaymentStubStartDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStubStartDateBusinessCenters":
					value = NoPaymentStubStartDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStubStartDateBusinessCenters = null;
		}
	}
}
