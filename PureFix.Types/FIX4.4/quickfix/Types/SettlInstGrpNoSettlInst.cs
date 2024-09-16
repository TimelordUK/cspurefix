using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SettlInstGrpNoSettlInst : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 162, Type = TagType.String, Offset = 0, Required = false)]
		public string? SettlInstID { get; set; }
		
		[TagDetails(Tag = 163, Type = TagType.String, Offset = 1, Required = false)]
		public string? SettlInstTransType { get; set; }
		
		[TagDetails(Tag = 214, Type = TagType.String, Offset = 2, Required = false)]
		public string? SettlInstRefID { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 4, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 5, Required = false)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 6, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 7, Required = false)]
		public string? CFICode { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 8, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 779, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? LastUpdateTime { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[TagDetails(Tag = 492, Type = TagType.Int, Offset = 12, Required = false)]
		public int? PaymentMethod { get; set; }
		
		[TagDetails(Tag = 476, Type = TagType.String, Offset = 13, Required = false)]
		public string? PaymentRef { get; set; }
		
		[TagDetails(Tag = 488, Type = TagType.String, Offset = 14, Required = false)]
		public string? CardHolderName { get; set; }
		
		[TagDetails(Tag = 489, Type = TagType.String, Offset = 15, Required = false)]
		public string? CardNumber { get; set; }
		
		[TagDetails(Tag = 503, Type = TagType.LocalDate, Offset = 16, Required = false)]
		public DateOnly? CardStartDate { get; set; }
		
		[TagDetails(Tag = 490, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateOnly? CardExpDate { get; set; }
		
		[TagDetails(Tag = 491, Type = TagType.String, Offset = 18, Required = false)]
		public string? CardIssNum { get; set; }
		
		[TagDetails(Tag = 504, Type = TagType.LocalDate, Offset = 19, Required = false)]
		public DateOnly? PaymentDate { get; set; }
		
		[TagDetails(Tag = 505, Type = TagType.String, Offset = 20, Required = false)]
		public string? PaymentRemitterID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SettlInstID is not null) writer.WriteString(162, SettlInstID);
			if (SettlInstTransType is not null) writer.WriteString(163, SettlInstTransType);
			if (SettlInstRefID is not null) writer.WriteString(214, SettlInstRefID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (Product is not null) writer.WriteWholeNumber(460, Product.Value);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (CFICode is not null) writer.WriteString(461, CFICode);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (LastUpdateTime is not null) writer.WriteUtcTimeStamp(779, LastUpdateTime.Value);
			if (SettlInstructionsData is not null) ((IFixEncoder)SettlInstructionsData).Encode(writer);
			if (PaymentMethod is not null) writer.WriteWholeNumber(492, PaymentMethod.Value);
			if (PaymentRef is not null) writer.WriteString(476, PaymentRef);
			if (CardHolderName is not null) writer.WriteString(488, CardHolderName);
			if (CardNumber is not null) writer.WriteString(489, CardNumber);
			if (CardStartDate is not null) writer.WriteLocalDateOnly(503, CardStartDate.Value);
			if (CardExpDate is not null) writer.WriteLocalDateOnly(490, CardExpDate.Value);
			if (CardIssNum is not null) writer.WriteString(491, CardIssNum);
			if (PaymentDate is not null) writer.WriteLocalDateOnly(504, PaymentDate.Value);
			if (PaymentRemitterID is not null) writer.WriteString(505, PaymentRemitterID);
		}
	}
}
