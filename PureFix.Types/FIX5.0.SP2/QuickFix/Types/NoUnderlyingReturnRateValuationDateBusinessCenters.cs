using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingReturnRateValuationDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 43070, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingReturnRateValuationDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingReturnRateValuationDateBusinessCenter is not null) writer.WriteString(43070, UnderlyingReturnRateValuationDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingReturnRateValuationDateBusinessCenter = view.GetString(43070);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingReturnRateValuationDateBusinessCenter":
					value = UnderlyingReturnRateValuationDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingReturnRateValuationDateBusinessCenter = null;
		}
	}
}
