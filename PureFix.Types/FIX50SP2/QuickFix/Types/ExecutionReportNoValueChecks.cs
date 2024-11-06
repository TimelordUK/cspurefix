using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExecutionReportNoValueChecks : IFixGroup
	{
		[TagDetails(Tag = 1869, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ValueCheckType {get; set;}
		
		[TagDetails(Tag = 1870, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ValueCheckAction {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ValueCheckType is not null) writer.WriteWholeNumber(1869, ValueCheckType.Value);
			if (ValueCheckAction is not null) writer.WriteWholeNumber(1870, ValueCheckAction.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ValueCheckType = view.GetInt32(1869);
			ValueCheckAction = view.GetInt32(1870);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ValueCheckType":
					value = ValueCheckType;
					break;
				case "ValueCheckAction":
					value = ValueCheckAction;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ValueCheckType = null;
			ValueCheckAction = null;
		}
	}
}
