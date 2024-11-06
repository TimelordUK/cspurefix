using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoMiscFees : IFixGroup
	{
		[TagDetails(Tag = 137, Type = TagType.Float, Offset = 0, Required = false)]
		public double? MiscFeeAmt {get; set;}
		
		[TagDetails(Tag = 138, Type = TagType.String, Offset = 1, Required = false)]
		public string? MiscFeeCurr {get; set;}
		
		[TagDetails(Tag = 139, Type = TagType.String, Offset = 2, Required = false)]
		public string? MiscFeeType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MiscFeeAmt is not null) writer.WriteNumber(137, MiscFeeAmt.Value);
			if (MiscFeeCurr is not null) writer.WriteString(138, MiscFeeCurr);
			if (MiscFeeType is not null) writer.WriteString(139, MiscFeeType);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MiscFeeAmt = view.GetDouble(137);
			MiscFeeCurr = view.GetString(138);
			MiscFeeType = view.GetString(139);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MiscFeeAmt":
					value = MiscFeeAmt;
					break;
				case "MiscFeeCurr":
					value = MiscFeeCurr;
					break;
				case "MiscFeeType":
					value = MiscFeeType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MiscFeeAmt = null;
			MiscFeeCurr = null;
			MiscFeeType = null;
		}
	}
}
