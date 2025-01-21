using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("AdjustedPositionReport", FixVersion.FIX50SP2)]
	public sealed partial class AdjustedPositionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosMaintRptID {get; set;}
		
		[TagDetails(Tag = 724, Type = TagType.Int, Offset = 2, Required = false)]
		public int? PosReqType {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 3, Required = true)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 4, Required = false)]
		public string? SettlSessID {get; set;}
		
		[TagDetails(Tag = 714, Type = TagType.String, Offset = 5, Required = false)]
		public string? PosMaintRptRefID {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 6, Required = true)]
		public AdjustedPositionReportParties[]? Parties {get; set;}
		
		[Group(NoOfTag = 1015, Offset = 7, Required = true)]
		public AdjustedPositionReportPositionQty[]? PositionQty {get; set;}
		
		[TagDetails(Tag = 730, Type = TagType.Float, Offset = 8, Required = false)]
		public double? SettlPrice {get; set;}
		
		[TagDetails(Tag = 734, Type = TagType.Float, Offset = 9, Required = false)]
		public double? PriorSettlPrice {get; set;}
		
		[Component(Offset = 10, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& PosMaintRptID is not null
				&& ClearingBusinessDate is not null
				&& Parties is not null && FixValidator.IsValid(Parties, in config)
				&& PositionQty is not null && FixValidator.IsValid(PositionQty, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (PosMaintRptID is not null) writer.WriteString(721, PosMaintRptID);
			if (PosReqType is not null) writer.WriteWholeNumber(724, PosReqType.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (PosMaintRptRefID is not null) writer.WriteString(714, PosMaintRptRefID);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (PositionQty is not null && PositionQty.Length != 0)
			{
				writer.WriteWholeNumber(1015, PositionQty.Length);
				for (int i = 0; i < PositionQty.Length; i++)
				{
					((IFixEncoder)PositionQty[i]).Encode(writer);
				}
			}
			if (SettlPrice is not null) writer.WriteNumber(730, SettlPrice.Value);
			if (PriorSettlPrice is not null) writer.WriteNumber(734, PriorSettlPrice.Value);
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
			PosMaintRptID = view.GetString(721);
			PosReqType = view.GetInt32(724);
			ClearingBusinessDate = view.GetDateOnly(715);
			SettlSessID = view.GetString(716);
			PosMaintRptRefID = view.GetString(714);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new AdjustedPositionReportParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			if (view.GetView("PositionQty") is IMessageView viewPositionQty)
			{
				var count = viewPositionQty.GroupCount();
				PositionQty = new AdjustedPositionReportPositionQty[count];
				for (int i = 0; i < count; i++)
				{
					PositionQty[i] = new();
					((IFixParser)PositionQty[i]).Parse(viewPositionQty.GetGroupInstance(i));
				}
			}
			SettlPrice = view.GetDouble(730);
			PriorSettlPrice = view.GetDouble(734);
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
				case "PosMaintRptID":
					value = PosMaintRptID;
					break;
				case "PosReqType":
					value = PosReqType;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "SettlSessID":
					value = SettlSessID;
					break;
				case "PosMaintRptRefID":
					value = PosMaintRptRefID;
					break;
				case "Parties":
					value = Parties;
					break;
				case "PositionQty":
					value = PositionQty;
					break;
				case "SettlPrice":
					value = SettlPrice;
					break;
				case "PriorSettlPrice":
					value = PriorSettlPrice;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			PosMaintRptID = null;
			PosReqType = null;
			ClearingBusinessDate = null;
			SettlSessID = null;
			PosMaintRptRefID = null;
			Parties = null;
			PositionQty = null;
			SettlPrice = null;
			PriorSettlPrice = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
