using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoMatchRules : IFixGroup
	{
		[TagDetails(Tag = 1142, Type = TagType.String, Offset = 0, Required = false)]
		public string? MatchAlgorithm {get; set;}
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 1, Required = false)]
		public string? MatchType {get; set;}
		
		[TagDetails(Tag = 2569, Type = TagType.String, Offset = 2, Required = false)]
		public string? MatchRuleProductComplex {get; set;}
		
		[TagDetails(Tag = 2570, Type = TagType.Int, Offset = 3, Required = false)]
		public int? CustomerPriority {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MatchAlgorithm is not null) writer.WriteString(1142, MatchAlgorithm);
			if (MatchType is not null) writer.WriteString(574, MatchType);
			if (MatchRuleProductComplex is not null) writer.WriteString(2569, MatchRuleProductComplex);
			if (CustomerPriority is not null) writer.WriteWholeNumber(2570, CustomerPriority.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MatchAlgorithm = view.GetString(1142);
			MatchType = view.GetString(574);
			MatchRuleProductComplex = view.GetString(2569);
			CustomerPriority = view.GetInt32(2570);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MatchAlgorithm":
					value = MatchAlgorithm;
					break;
				case "MatchType":
					value = MatchType;
					break;
				case "MatchRuleProductComplex":
					value = MatchRuleProductComplex;
					break;
				case "CustomerPriority":
					value = CustomerPriority;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MatchAlgorithm = null;
			MatchType = null;
			MatchRuleProductComplex = null;
			CustomerPriority = null;
		}
	}
}
