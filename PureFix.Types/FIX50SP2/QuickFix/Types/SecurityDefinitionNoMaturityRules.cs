using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityDefinitionNoMaturityRules : IFixGroup
	{
		[TagDetails(Tag = 1222, Type = TagType.String, Offset = 0, Required = false)]
		public string? MaturityRuleID {get; set;}
		
		[TagDetails(Tag = 1303, Type = TagType.Int, Offset = 1, Required = false)]
		public int? MaturityMonthYearFormat {get; set;}
		
		[TagDetails(Tag = 1302, Type = TagType.Int, Offset = 2, Required = false)]
		public int? MaturityMonthYearIncrementUnits {get; set;}
		
		[TagDetails(Tag = 1241, Type = TagType.MonthYear, Offset = 3, Required = false)]
		public MonthYear? StartMaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 1226, Type = TagType.MonthYear, Offset = 4, Required = false)]
		public MonthYear? EndMaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 1229, Type = TagType.Int, Offset = 5, Required = false)]
		public int? MaturityMonthYearIncrement {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MaturityRuleID is not null) writer.WriteString(1222, MaturityRuleID);
			if (MaturityMonthYearFormat is not null) writer.WriteWholeNumber(1303, MaturityMonthYearFormat.Value);
			if (MaturityMonthYearIncrementUnits is not null) writer.WriteWholeNumber(1302, MaturityMonthYearIncrementUnits.Value);
			if (StartMaturityMonthYear is not null) writer.WriteMonthYear(1241, StartMaturityMonthYear.Value);
			if (EndMaturityMonthYear is not null) writer.WriteMonthYear(1226, EndMaturityMonthYear.Value);
			if (MaturityMonthYearIncrement is not null) writer.WriteWholeNumber(1229, MaturityMonthYearIncrement.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MaturityRuleID = view.GetString(1222);
			MaturityMonthYearFormat = view.GetInt32(1303);
			MaturityMonthYearIncrementUnits = view.GetInt32(1302);
			StartMaturityMonthYear = view.GetMonthYear(1241);
			EndMaturityMonthYear = view.GetMonthYear(1226);
			MaturityMonthYearIncrement = view.GetInt32(1229);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MaturityRuleID":
					value = MaturityRuleID;
					break;
				case "MaturityMonthYearFormat":
					value = MaturityMonthYearFormat;
					break;
				case "MaturityMonthYearIncrementUnits":
					value = MaturityMonthYearIncrementUnits;
					break;
				case "StartMaturityMonthYear":
					value = StartMaturityMonthYear;
					break;
				case "EndMaturityMonthYear":
					value = EndMaturityMonthYear;
					break;
				case "MaturityMonthYearIncrement":
					value = MaturityMonthYearIncrement;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MaturityRuleID = null;
			MaturityMonthYearFormat = null;
			MaturityMonthYearIncrementUnits = null;
			StartMaturityMonthYear = null;
			EndMaturityMonthYear = null;
			MaturityMonthYearIncrement = null;
		}
	}
}
