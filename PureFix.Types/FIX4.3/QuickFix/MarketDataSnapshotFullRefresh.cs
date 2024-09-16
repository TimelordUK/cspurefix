using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("W", FixVersion.FIX43)]
	public sealed partial class MarketDataSnapshotFullRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = false)]
		public string? MDReqID { get; set; }
		
		[Component(Offset = 2, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 3, Required = false)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 4, Required = false)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(Tag = 387, Type = TagType.Float, Offset = 5, Required = false)]
		public double? TotalVolumeTraded { get; set; }
		
		[TagDetails(Tag = 449, Type = TagType.String, Offset = 6, Required = false)]
		public string? TotalVolumeTradedDate { get; set; }
		
		[TagDetails(Tag = 450, Type = TagType.UtcTimeOnly, Offset = 7, Required = false)]
		public TimeOnly? TotalVolumeTradedTime { get; set; }
		
		[TagDetails(Tag = 451, Type = TagType.Float, Offset = 8, Required = false)]
		public double? NetChgPrevDay { get; set; }
		
		[Group(NoOfTag = 268, Offset = 9, Required = true)]
		public MarketDataSnapshotFullRefreshNoMDEntries[]? NoMDEntries { get; set; }
		
		[Component(Offset = 10, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& NoMDEntries is not null && FixValidator.IsValid(NoMDEntries, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
			if (TotalVolumeTraded is not null) writer.WriteNumber(387, TotalVolumeTraded.Value);
			if (TotalVolumeTradedDate is not null) writer.WriteString(449, TotalVolumeTradedDate);
			if (TotalVolumeTradedTime is not null) writer.WriteTimeOnly(450, TotalVolumeTradedTime.Value);
			if (NetChgPrevDay is not null) writer.WriteNumber(451, NetChgPrevDay.Value);
			if (NoMDEntries is not null && NoMDEntries.Length != 0)
			{
				writer.WriteWholeNumber(268, NoMDEntries.Length);
				for (int i = 0; i < NoMDEntries.Length; i++)
				{
					((IFixEncoder)NoMDEntries[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
