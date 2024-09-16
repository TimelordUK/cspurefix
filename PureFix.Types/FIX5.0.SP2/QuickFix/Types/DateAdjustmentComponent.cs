using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DateAdjustmentComponent : IFixComponent
	{
		[TagDetails(Tag = 40921, Type = TagType.Int, Offset = 0, Required = false)]
		public int? BusinessDayConvention {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public BusinessCenterGrpComponent? BusinessCenterGrp {get; set;}
		
		[TagDetails(Tag = 40922, Type = TagType.String, Offset = 2, Required = false)]
		public string? DateRollConvention {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (BusinessDayConvention is not null) writer.WriteWholeNumber(40921, BusinessDayConvention.Value);
			if (BusinessCenterGrp is not null) ((IFixEncoder)BusinessCenterGrp).Encode(writer);
			if (DateRollConvention is not null) writer.WriteString(40922, DateRollConvention);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			BusinessDayConvention = view.GetInt32(40921);
			if (view.GetView("BusinessCenterGrp") is IMessageView viewBusinessCenterGrp)
			{
				BusinessCenterGrp = new();
				((IFixParser)BusinessCenterGrp).Parse(viewBusinessCenterGrp);
			}
			DateRollConvention = view.GetString(40922);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "BusinessDayConvention":
					value = BusinessDayConvention;
					break;
				case "BusinessCenterGrp":
					value = BusinessCenterGrp;
					break;
				case "DateRollConvention":
					value = DateRollConvention;
					break;
				default: return false;
			}
			return true;
		}
	}
}
