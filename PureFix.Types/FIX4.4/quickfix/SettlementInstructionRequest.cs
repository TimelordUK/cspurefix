using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AV", FixVersion.FIX44)]
	public sealed partial class SettlementInstructionRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 791, Type = TagType.String, Offset = 1, Required = true)]
		public string? SettlInstReqID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 2, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 4, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 5, Required = false)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 6, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 7, Required = false)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 9, Required = false)]
		public string? CFICode { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 11, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 779, Type = TagType.UtcTimestamp, Offset = 12, Required = false)]
		public DateTime? LastUpdateTime { get; set; }
		
		[TagDetails(Tag = 169, Type = TagType.Int, Offset = 13, Required = false)]
		public int? StandInstDbType { get; set; }
		
		[TagDetails(Tag = 170, Type = TagType.String, Offset = 14, Required = false)]
		public string? StandInstDbName { get; set; }
		
		[TagDetails(Tag = 171, Type = TagType.String, Offset = 15, Required = false)]
		public string? StandInstDbID { get; set; }
		
		[Component(Offset = 16, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SettlInstReqID is not null
				&& TransactTime is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SettlInstReqID is not null) writer.WriteString(791, SettlInstReqID);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (AllocAcctIDSource is not null) writer.WriteWholeNumber(661, AllocAcctIDSource.Value);
			if (Side is not null) writer.WriteString(54, Side);
			if (Product is not null) writer.WriteWholeNumber(460, Product.Value);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (CFICode is not null) writer.WriteString(461, CFICode);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (LastUpdateTime is not null) writer.WriteUtcTimeStamp(779, LastUpdateTime.Value);
			if (StandInstDbType is not null) writer.WriteWholeNumber(169, StandInstDbType.Value);
			if (StandInstDbName is not null) writer.WriteString(170, StandInstDbName);
			if (StandInstDbID is not null) writer.WriteString(171, StandInstDbID);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
