using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoTransactionAttributes : IFixGroup
	{
		[TagDetails(Tag = 2872, Type = TagType.Int, Offset = 0, Required = false)]
		public int? TransactionAttributeType {get; set;}
		
		[TagDetails(Tag = 2873, Type = TagType.String, Offset = 1, Required = false)]
		public string? TransactionAttributeValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TransactionAttributeType is not null) writer.WriteWholeNumber(2872, TransactionAttributeType.Value);
			if (TransactionAttributeValue is not null) writer.WriteString(2873, TransactionAttributeValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TransactionAttributeType = view.GetInt32(2872);
			TransactionAttributeValue = view.GetString(2873);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TransactionAttributeType":
					value = TransactionAttributeType;
					break;
				case "TransactionAttributeValue":
					value = TransactionAttributeValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TransactionAttributeType = null;
			TransactionAttributeValue = null;
		}
	}
}
