using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoRequestedRiskLimitType : IFixGroup
	{
		[TagDetails(Tag = 1530, Type = TagType.Int, Offset = 0, Required = false)]
		public int? RiskLimitType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RiskLimitType is not null) writer.WriteWholeNumber(1530, RiskLimitType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RiskLimitType = view.GetInt32(1530);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RiskLimitType":
					value = RiskLimitType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RiskLimitType = null;
		}
	}
}
