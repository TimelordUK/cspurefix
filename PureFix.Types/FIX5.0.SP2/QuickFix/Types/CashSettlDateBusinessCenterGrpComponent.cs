using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class CashSettlDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42214, Offset = 0, Required = false)]
		public NoCashSettlDateBusinessCenters[]? NoCashSettlDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCashSettlDateBusinessCenters is not null && NoCashSettlDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42214, NoCashSettlDateBusinessCenters.Length);
				for (int i = 0; i < NoCashSettlDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoCashSettlDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCashSettlDateBusinessCenters") is IMessageView viewNoCashSettlDateBusinessCenters)
			{
				var count = viewNoCashSettlDateBusinessCenters.GroupCount();
				NoCashSettlDateBusinessCenters = new NoCashSettlDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoCashSettlDateBusinessCenters[i] = new();
					((IFixParser)NoCashSettlDateBusinessCenters[i]).Parse(viewNoCashSettlDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCashSettlDateBusinessCenters":
					value = NoCashSettlDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
