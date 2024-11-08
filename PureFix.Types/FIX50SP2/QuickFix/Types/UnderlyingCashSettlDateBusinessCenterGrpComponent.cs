using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingCashSettlDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42788, Offset = 0, Required = false)]
		public IOINoUnderlyingCashSettlDateBusinessCenters[]? NoUnderlyingCashSettlDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingCashSettlDateBusinessCenters is not null && NoUnderlyingCashSettlDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42788, NoUnderlyingCashSettlDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingCashSettlDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingCashSettlDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingCashSettlDateBusinessCenters") is IMessageView viewNoUnderlyingCashSettlDateBusinessCenters)
			{
				var count = viewNoUnderlyingCashSettlDateBusinessCenters.GroupCount();
				NoUnderlyingCashSettlDateBusinessCenters = new IOINoUnderlyingCashSettlDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingCashSettlDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingCashSettlDateBusinessCenters[i]).Parse(viewNoUnderlyingCashSettlDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingCashSettlDateBusinessCenters":
					value = NoUnderlyingCashSettlDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingCashSettlDateBusinessCenters = null;
		}
	}
}
