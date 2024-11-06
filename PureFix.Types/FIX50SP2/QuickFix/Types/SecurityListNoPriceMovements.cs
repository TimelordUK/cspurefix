using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityListNoPriceMovements : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public PriceMovementValueGrpComponent? PriceMovementValueGrp {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ClearingAccountTypeGrpComponent? ClearingAccountTypeGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PriceMovementValueGrp is not null) ((IFixEncoder)PriceMovementValueGrp).Encode(writer);
			if (ClearingAccountTypeGrp is not null) ((IFixEncoder)ClearingAccountTypeGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("PriceMovementValueGrp") is IMessageView viewPriceMovementValueGrp)
			{
				PriceMovementValueGrp = new();
				((IFixParser)PriceMovementValueGrp).Parse(viewPriceMovementValueGrp);
			}
			if (view.GetView("ClearingAccountTypeGrp") is IMessageView viewClearingAccountTypeGrp)
			{
				ClearingAccountTypeGrp = new();
				((IFixParser)ClearingAccountTypeGrp).Parse(viewClearingAccountTypeGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PriceMovementValueGrp":
					value = PriceMovementValueGrp;
					break;
				case "ClearingAccountTypeGrp":
					value = ClearingAccountTypeGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)PriceMovementValueGrp)?.Reset();
			((IFixReset?)ClearingAccountTypeGrp)?.Reset();
		}
	}
}
