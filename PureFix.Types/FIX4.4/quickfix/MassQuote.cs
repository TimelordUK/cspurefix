using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("i", FixVersion.FIX44)]
	public sealed partial class MassQuote : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = true)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 3, Required = false)]
		public int? QuoteType { get; set; }
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteResponseLevel { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 7, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 8, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 293, Type = TagType.Float, Offset = 9, Required = false)]
		public double? DefBidSize { get; set; }
		
		[TagDetails(Tag = 294, Type = TagType.Float, Offset = 10, Required = false)]
		public double? DefOfferSize { get; set; }
		
		[Component(Offset = 11, Required = true)]
		public QuotSetGrp? QuotSetGrp { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteID is not null
				&& QuotSetGrp is not null && ((IFixValidator)QuotSetGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DefBidSize is not null) writer.WriteNumber(293, DefBidSize.Value);
			if (DefOfferSize is not null) writer.WriteNumber(294, DefOfferSize.Value);
			if (QuotSetGrp is not null) ((IFixEncoder)QuotSetGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
