using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("OrderMassActionReport", FixVersion.FIX50SP2)]
	public sealed partial class OrderMassActionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 1369, Type = TagType.String, Offset = 3, Required = true)]
		public string? MassActionReportID {get; set;}
		
		[TagDetails(Tag = 1373, Type = TagType.Int, Offset = 4, Required = true)]
		public int? MassActionType {get; set;}
		
		[TagDetails(Tag = 1374, Type = TagType.Int, Offset = 5, Required = true)]
		public int? MassActionScope {get; set;}
		
		[TagDetails(Tag = 1375, Type = TagType.Int, Offset = 6, Required = true)]
		public int? MassActionResponse {get; set;}
		
		[TagDetails(Tag = 1376, Type = TagType.Int, Offset = 7, Required = false)]
		public int? MassActionRejectReason {get; set;}
		
		[TagDetails(Tag = 533, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TotalAffectedOrders {get; set;}
		
		[TagDetails(Tag = 1301, Type = TagType.String, Offset = 9, Required = false)]
		public string? MarketID {get; set;}
		
		[TagDetails(Tag = 1300, Type = TagType.String, Offset = 10, Required = false)]
		public string? MarketSegmentID {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 12, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 13, Required = false)]
		public OrderMassActionReportParties[]? Parties {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 15, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 16, Required = false)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 17, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 18, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 19, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 20, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 21, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[Group(NoOfTag = 1063, Offset = 22, Required = false)]
		public OrderMassActionReportTargetParties[]? TargetParties {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& MassActionReportID is not null
				&& MassActionType is not null
				&& MassActionScope is not null
				&& MassActionResponse is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (MassActionReportID is not null) writer.WriteString(1369, MassActionReportID);
			if (MassActionType is not null) writer.WriteWholeNumber(1373, MassActionType.Value);
			if (MassActionScope is not null) writer.WriteWholeNumber(1374, MassActionScope.Value);
			if (MassActionResponse is not null) writer.WriteWholeNumber(1375, MassActionResponse.Value);
			if (MassActionRejectReason is not null) writer.WriteWholeNumber(1376, MassActionRejectReason.Value);
			if (TotalAffectedOrders is not null) writer.WriteWholeNumber(533, TotalAffectedOrders.Value);
			if (MarketID is not null) writer.WriteString(1301, MarketID);
			if (MarketSegmentID is not null) writer.WriteString(1300, MarketSegmentID);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (TargetParties is not null && TargetParties.Length != 0)
			{
				writer.WriteWholeNumber(1063, TargetParties.Length);
				for (int i = 0; i < TargetParties.Length; i++)
				{
					((IFixEncoder)TargetParties[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			ClOrdID = view.GetString(11);
			SecondaryClOrdID = view.GetString(526);
			MassActionReportID = view.GetString(1369);
			MassActionType = view.GetInt32(1373);
			MassActionScope = view.GetInt32(1374);
			MassActionResponse = view.GetInt32(1375);
			MassActionRejectReason = view.GetInt32(1376);
			TotalAffectedOrders = view.GetInt32(533);
			MarketID = view.GetString(1301);
			MarketSegmentID = view.GetString(1300);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new OrderMassActionReportParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("UnderlyingInstrument") is IMessageView viewUnderlyingInstrument)
			{
				UnderlyingInstrument = new();
				((IFixParser)UnderlyingInstrument).Parse(viewUnderlyingInstrument);
			}
			Side = view.GetString(54);
			TransactTime = view.GetDateTime(60);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			if (view.GetView("TargetParties") is IMessageView viewTargetParties)
			{
				var count = viewTargetParties.GroupCount();
				TargetParties = new OrderMassActionReportTargetParties[count];
				for (int i = 0; i < count; i++)
				{
					TargetParties[i] = new();
					((IFixParser)TargetParties[i]).Parse(viewTargetParties.GetGroupInstance(i));
				}
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
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "MassActionReportID":
					value = MassActionReportID;
					break;
				case "MassActionType":
					value = MassActionType;
					break;
				case "MassActionScope":
					value = MassActionScope;
					break;
				case "MassActionResponse":
					value = MassActionResponse;
					break;
				case "MassActionRejectReason":
					value = MassActionRejectReason;
					break;
				case "TotalAffectedOrders":
					value = TotalAffectedOrders;
					break;
				case "MarketID":
					value = MarketID;
					break;
				case "MarketSegmentID":
					value = MarketSegmentID;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "Parties":
					value = Parties;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "UnderlyingInstrument":
					value = UnderlyingInstrument;
					break;
				case "Side":
					value = Side;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				case "TargetParties":
					value = TargetParties;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			ClOrdID = null;
			SecondaryClOrdID = null;
			MassActionReportID = null;
			MassActionType = null;
			MassActionScope = null;
			MassActionResponse = null;
			MassActionRejectReason = null;
			TotalAffectedOrders = null;
			MarketID = null;
			MarketSegmentID = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			Parties = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)UnderlyingInstrument)?.Reset();
			Side = null;
			TransactTime = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
			TargetParties = null;
		}
	}
}
