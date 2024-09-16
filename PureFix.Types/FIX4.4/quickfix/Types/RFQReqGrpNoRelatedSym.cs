using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class RFQReqGrpNoRelatedSym : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 1, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 3, Required = false)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 303, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRequestType { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteType { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 7, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Instrument is not null && ((IFixValidator)Instrument).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (QuoteRequestType is not null) writer.WriteWholeNumber(303, QuoteRequestType.Value);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
		}
	}
}
