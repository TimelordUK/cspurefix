using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AA", FixVersion.FIX44)]
	public sealed partial class DerivativeSecurityList : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID {get; set;}
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2, Required = true)]
		public string? SecurityResponseID {get; set;}
		
		[TagDetails(Tag = 560, Type = TagType.Int, Offset = 3, Required = true)]
		public int? SecurityRequestResult {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TotNoRelatedSym {get; set;}
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 6, Required = false)]
		public bool? LastFragment {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public RelSymDerivSecGrpComponent? RelSymDerivSecGrp {get; set;}
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SecurityReqID is not null
				&& SecurityResponseID is not null
				&& SecurityRequestResult is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityResponseID is not null) writer.WriteString(322, SecurityResponseID);
			if (SecurityRequestResult is not null) writer.WriteWholeNumber(560, SecurityRequestResult.Value);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (TotNoRelatedSym is not null) writer.WriteWholeNumber(393, TotNoRelatedSym.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (RelSymDerivSecGrp is not null) ((IFixEncoder)RelSymDerivSecGrp).Encode(writer);
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
			SecurityReqID = view.GetString(320);
			SecurityResponseID = view.GetString(322);
			SecurityRequestResult = view.GetInt32(560);
			if (view.GetView("UnderlyingInstrument") is IMessageView viewUnderlyingInstrument)
			{
				UnderlyingInstrument = new();
				((IFixParser)UnderlyingInstrument).Parse(viewUnderlyingInstrument);
			}
			TotNoRelatedSym = view.GetInt32(393);
			LastFragment = view.GetBool(893);
			if (view.GetView("RelSymDerivSecGrp") is IMessageView viewRelSymDerivSecGrp)
			{
				RelSymDerivSecGrp = new();
				((IFixParser)RelSymDerivSecGrp).Parse(viewRelSymDerivSecGrp);
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
				case "SecurityReqID":
					value = SecurityReqID;
					break;
				case "SecurityResponseID":
					value = SecurityResponseID;
					break;
				case "SecurityRequestResult":
					value = SecurityRequestResult;
					break;
				case "UnderlyingInstrument":
					value = UnderlyingInstrument;
					break;
				case "TotNoRelatedSym":
					value = TotNoRelatedSym;
					break;
				case "LastFragment":
					value = LastFragment;
					break;
				case "RelSymDerivSecGrp":
					value = RelSymDerivSecGrp;
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
			SecurityReqID = null;
			SecurityResponseID = null;
			SecurityRequestResult = null;
			((IFixReset?)UnderlyingInstrument)?.Reset();
			TotNoRelatedSym = null;
			LastFragment = null;
			((IFixReset?)RelSymDerivSecGrp)?.Reset();
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
