using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegCashSettlDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42307, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegCashSettlDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegCashSettlDateBusinessCenter is not null) writer.WriteString(42307, LegCashSettlDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegCashSettlDateBusinessCenter = view.GetString(42307);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegCashSettlDateBusinessCenter":
					value = LegCashSettlDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegCashSettlDateBusinessCenter = null;
		}
	}
}
