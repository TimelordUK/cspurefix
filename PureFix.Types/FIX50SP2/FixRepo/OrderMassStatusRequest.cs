using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("OrderMassStatusRequest", FixVersion.FIX50SP2)]
	public sealed partial class OrderMassStatusRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 584, Type = TagType.String, Offset = 1, Required = true)]
		public string? MassStatusReqID {get; set;}
		
		[TagDetails(Tag = 585, Type = TagType.Int, Offset = 2, Required = true)]
		public int? MassStatusReqType {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 3, Required = false)]
		public OrderMassStatusRequestParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 4, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 5, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 7, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[Component(Offset = 8, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 9, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 10, Required = false)]
		public string? Side {get; set;}
		
		[Component(Offset = 11, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[Group(NoOfTag = 1063, Offset = 12, Required = false)]
		public OrderMassStatusRequestTargetParties[]? TargetParties {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& MassStatusReqID is not null
				&& MassStatusReqType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (MassStatusReqID is not null) writer.WriteString(584, MassStatusReqID);
			if (MassStatusReqType is not null) writer.WriteWholeNumber(585, MassStatusReqType.Value);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
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
			MassStatusReqID = view.GetString(584);
			MassStatusReqType = view.GetInt32(585);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new OrderMassStatusRequestParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
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
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			if (view.GetView("TargetParties") is IMessageView viewTargetParties)
			{
				var count = viewTargetParties.GroupCount();
				TargetParties = new OrderMassStatusRequestTargetParties[count];
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
				case "MassStatusReqID":
					value = MassStatusReqID;
					break;
				case "MassStatusReqType":
					value = MassStatusReqType;
					break;
				case "Parties":
					value = Parties;
					break;
				case "Account":
					value = Account;
					break;
				case "AcctIDSource":
					value = AcctIDSource;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
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
			MassStatusReqID = null;
			MassStatusReqType = null;
			Parties = null;
			Account = null;
			AcctIDSource = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)UnderlyingInstrument)?.Reset();
			Side = null;
			((IFixReset?)StandardTrailer)?.Reset();
			TargetParties = null;
		}
	}
}
