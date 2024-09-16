using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class OrdListStatGrpNoOrders : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 2, Required = true)]
		public double? CumQty { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 3, Required = true)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? WorkingIndicator { get; set; }
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 5, Required = true)]
		public double? LeavesQty { get; set; }
		
		[TagDetails(Tag = 84, Type = TagType.Float, Offset = 6, Required = true)]
		public double? CxlQty { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 7, Required = true)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 8, Required = false)]
		public int? OrdRejReason { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 9, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 10, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 11, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				ClOrdID is not null
				&& CumQty is not null
				&& OrdStatus is not null
				&& LeavesQty is not null
				&& CxlQty is not null
				&& AvgPx is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (CumQty is not null) writer.WriteNumber(14, CumQty.Value);
			if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
			if (WorkingIndicator is not null) writer.WriteBoolean(636, WorkingIndicator.Value);
			if (LeavesQty is not null) writer.WriteNumber(151, LeavesQty.Value);
			if (CxlQty is not null) writer.WriteNumber(84, CxlQty.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (OrdRejReason is not null) writer.WriteWholeNumber(103, OrdRejReason.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
		}
	}
}
