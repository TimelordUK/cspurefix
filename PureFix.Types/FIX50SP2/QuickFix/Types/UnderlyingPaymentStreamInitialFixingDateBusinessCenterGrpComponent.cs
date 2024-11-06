using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamInitialFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40971, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentStreamInitialFixingDateBusinessCenters[]? NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters is not null && NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40971, NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters") is IMessageView viewNoUnderlyingPaymentStreamInitialFixingDateBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentStreamInitialFixingDateBusinessCenters.GroupCount();
				NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters = new IOINoUnderlyingPaymentStreamInitialFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters[i]).Parse(viewNoUnderlyingPaymentStreamInitialFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters":
					value = NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStreamInitialFixingDateBusinessCenters = null;
		}
	}
}
