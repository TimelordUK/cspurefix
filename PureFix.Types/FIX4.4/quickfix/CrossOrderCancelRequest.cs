using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("u", FixVersion.FIX44)]
	public sealed partial class CrossOrderCancelRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 2, Required = true)]
		public string? CrossID {get; set;}
		
		[TagDetails(Tag = 551, Type = TagType.String, Offset = 3, Required = true)]
		public string? OrigCrossID {get; set;}
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 4, Required = true)]
		public int? CrossType {get; set;}
		
		[TagDetails(Tag = 550, Type = TagType.Int, Offset = 5, Required = true)]
		public int? CrossPrioritization {get; set;}
		
		[Component(Offset = 6, Required = true)]
		public SideCrossOrdCxlGrpComponent? SideCrossOrdCxlGrp {get; set;}
		
		[Component(Offset = 7, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 8, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 9, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 10, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 11, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& CrossID is not null
				&& OrigCrossID is not null
				&& CrossType is not null
				&& CrossPrioritization is not null
				&& SideCrossOrdCxlGrp is not null && ((IFixValidator)SideCrossOrdCxlGrp).IsValid(in config)
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& TransactTime is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (CrossID is not null) writer.WriteString(548, CrossID);
			if (OrigCrossID is not null) writer.WriteString(551, OrigCrossID);
			if (CrossType is not null) writer.WriteWholeNumber(549, CrossType.Value);
			if (CrossPrioritization is not null) writer.WriteWholeNumber(550, CrossPrioritization.Value);
			if (SideCrossOrdCxlGrp is not null) ((IFixEncoder)SideCrossOrdCxlGrp).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			OrderID = view.GetString(37);
			CrossID = view.GetString(548);
			OrigCrossID = view.GetString(551);
			CrossType = view.GetInt32(549);
			CrossPrioritization = view.GetInt32(550);
			if (view.GetView("SideCrossOrdCxlGrp") is IMessageView viewSideCrossOrdCxlGrp)
			{
				SideCrossOrdCxlGrp = new();
				((IFixParser)SideCrossOrdCxlGrp).Parse(viewSideCrossOrdCxlGrp);
			}
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			TransactTime = view.GetDateTime(60);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
					value = StandardHeader;
					break;
				case "OrderID":
					value = OrderID;
					break;
				case "CrossID":
					value = CrossID;
					break;
				case "OrigCrossID":
					value = OrigCrossID;
					break;
				case "CrossType":
					value = CrossType;
					break;
				case "CrossPrioritization":
					value = CrossPrioritization;
					break;
				case "SideCrossOrdCxlGrp":
					value = SideCrossOrdCxlGrp;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
	}
}
