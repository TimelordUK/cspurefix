using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExecutionReportNoMiscFeeSubTypes : IFixGroup
	{
		[TagDetails(Tag = 2634, Type = TagType.String, Offset = 0, Required = false)]
		public string? MiscFeeSubType {get; set;}
		
		[TagDetails(Tag = 2635, Type = TagType.Float, Offset = 1, Required = false)]
		public double? MiscFeeSubTypeAmt {get; set;}
		
		[TagDetails(Tag = 2636, Type = TagType.String, Offset = 2, Required = false)]
		public string? MiscFeeSubTypeDesc {get; set;}
		
		[TagDetails(Tag = 2637, Type = TagType.Length, Offset = 3, Required = false, LinksToTag = 2638)]
		public int? EncodedMiscFeeSubTypeDescLen {get; set;}
		
		[TagDetails(Tag = 2638, Type = TagType.RawData, Offset = 4, Required = false, LinksToTag = 2637)]
		public byte[]? EncodedMiscFeeSubTypeDesc {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MiscFeeSubType is not null) writer.WriteString(2634, MiscFeeSubType);
			if (MiscFeeSubTypeAmt is not null) writer.WriteNumber(2635, MiscFeeSubTypeAmt.Value);
			if (MiscFeeSubTypeDesc is not null) writer.WriteString(2636, MiscFeeSubTypeDesc);
			if (EncodedMiscFeeSubTypeDesc is not null)
			{
				writer.WriteWholeNumber(2637, EncodedMiscFeeSubTypeDesc.Length);
				writer.WriteBuffer(2638, EncodedMiscFeeSubTypeDesc);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MiscFeeSubType = view.GetString(2634);
			MiscFeeSubTypeAmt = view.GetDouble(2635);
			MiscFeeSubTypeDesc = view.GetString(2636);
			EncodedMiscFeeSubTypeDescLen = view.GetInt32(2637);
			EncodedMiscFeeSubTypeDesc = view.GetByteArray(2638);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MiscFeeSubType":
					value = MiscFeeSubType;
					break;
				case "MiscFeeSubTypeAmt":
					value = MiscFeeSubTypeAmt;
					break;
				case "MiscFeeSubTypeDesc":
					value = MiscFeeSubTypeDesc;
					break;
				case "EncodedMiscFeeSubTypeDescLen":
					value = EncodedMiscFeeSubTypeDescLen;
					break;
				case "EncodedMiscFeeSubTypeDesc":
					value = EncodedMiscFeeSubTypeDesc;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MiscFeeSubType = null;
			MiscFeeSubTypeAmt = null;
			MiscFeeSubTypeDesc = null;
			EncodedMiscFeeSubTypeDescLen = null;
			EncodedMiscFeeSubTypeDesc = null;
		}
	}
}
