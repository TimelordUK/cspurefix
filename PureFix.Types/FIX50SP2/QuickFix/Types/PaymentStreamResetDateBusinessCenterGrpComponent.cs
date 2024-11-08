using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamResetDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40948, Offset = 0, Required = false)]
		public IOINoPaymentStreamResetDateBusinessCenters[]? NoPaymentStreamResetDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamResetDateBusinessCenters is not null && NoPaymentStreamResetDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40948, NoPaymentStreamResetDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStreamResetDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamResetDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamResetDateBusinessCenters") is IMessageView viewNoPaymentStreamResetDateBusinessCenters)
			{
				var count = viewNoPaymentStreamResetDateBusinessCenters.GroupCount();
				NoPaymentStreamResetDateBusinessCenters = new IOINoPaymentStreamResetDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamResetDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentStreamResetDateBusinessCenters[i]).Parse(viewNoPaymentStreamResetDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamResetDateBusinessCenters":
					value = NoPaymentStreamResetDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStreamResetDateBusinessCenters = null;
		}
	}
}
