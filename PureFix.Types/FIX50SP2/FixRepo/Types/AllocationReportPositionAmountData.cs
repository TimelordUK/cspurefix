using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class AllocationReportPositionAmountData : IFixGroup
	{
		[TagDetails(Tag = 707, Type = TagType.String, Offset = 0, Required = false)]
		public string? PosAmtType {get; set;}
		
		[TagDetails(Tag = 708, Type = TagType.Float, Offset = 1, Required = false)]
		public double? PosAmt {get; set;}
		
		[TagDetails(Tag = 1055, Type = TagType.String, Offset = 2, Required = false)]
		public string? PositionCurrency {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PosAmtType is not null) writer.WriteString(707, PosAmtType);
			if (PosAmt is not null) writer.WriteNumber(708, PosAmt.Value);
			if (PositionCurrency is not null) writer.WriteString(1055, PositionCurrency);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PosAmtType = view.GetString(707);
			PosAmt = view.GetDouble(708);
			PositionCurrency = view.GetString(1055);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PosAmtType":
					value = PosAmtType;
					break;
				case "PosAmt":
					value = PosAmt;
					break;
				case "PositionCurrency":
					value = PositionCurrency;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PosAmtType = null;
			PosAmt = null;
			PositionCurrency = null;
		}
	}
}
