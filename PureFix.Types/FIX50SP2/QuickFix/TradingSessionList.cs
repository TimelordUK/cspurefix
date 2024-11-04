using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix
{
	[MessageType("BJ", FixVersion.FIX50SP2)]
	public sealed partial class TradingSessionList : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 335, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradSesReqID {get; set;}
		
		[Component(Offset = 3, Required = true)]
		public TrdSessLstGrpComponent? TrdSessLstGrp {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TrdSessLstGrp is not null && ((IFixValidator)TrdSessLstGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (TradSesReqID is not null) writer.WriteString(335, TradSesReqID);
			if (TrdSessLstGrp is not null) ((IFixEncoder)TrdSessLstGrp).Encode(writer);
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
			if (view.GetView("ApplicationSequenceControl") is IMessageView viewApplicationSequenceControl)
			{
				ApplicationSequenceControl = new();
				((IFixParser)ApplicationSequenceControl).Parse(viewApplicationSequenceControl);
			}
			TradSesReqID = view.GetString(335);
			if (view.GetView("TrdSessLstGrp") is IMessageView viewTrdSessLstGrp)
			{
				TrdSessLstGrp = new();
				((IFixParser)TrdSessLstGrp).Parse(viewTrdSessLstGrp);
			}
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
				case "ApplicationSequenceControl":
					value = ApplicationSequenceControl;
					break;
				case "TradSesReqID":
					value = TradSesReqID;
					break;
				case "TrdSessLstGrp":
					value = TrdSessLstGrp;
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
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			TradSesReqID = null;
			((IFixReset?)TrdSessLstGrp)?.Reset();
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
