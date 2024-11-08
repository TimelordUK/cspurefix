using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamInitialFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40949, Offset = 0, Required = false)]
		public IOINoPaymentStreamInitialFixingDateBusinessCenters[]? NoPaymentStreamInitialFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamInitialFixingDateBusinessCenters is not null && NoPaymentStreamInitialFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40949, NoPaymentStreamInitialFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStreamInitialFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamInitialFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamInitialFixingDateBusinessCenters") is IMessageView viewNoPaymentStreamInitialFixingDateBusinessCenters)
			{
				var count = viewNoPaymentStreamInitialFixingDateBusinessCenters.GroupCount();
				NoPaymentStreamInitialFixingDateBusinessCenters = new IOINoPaymentStreamInitialFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamInitialFixingDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentStreamInitialFixingDateBusinessCenters[i]).Parse(viewNoPaymentStreamInitialFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamInitialFixingDateBusinessCenters":
					value = NoPaymentStreamInitialFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStreamInitialFixingDateBusinessCenters = null;
		}
	}
}
