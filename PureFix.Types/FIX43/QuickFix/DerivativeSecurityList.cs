using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AA", FixVersion.FIX43)]
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
		public int? TotalNumSecurities {get; set;}
		
		[Group(NoOfTag = 146, Offset = 6, Required = false)]
		public DerivativeSecurityListNoRelatedSym[]? NoRelatedSym {get; set;}
		
		[Component(Offset = 7, Required = true)]
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
			if (TotalNumSecurities is not null) writer.WriteWholeNumber(393, TotalNumSecurities.Value);
			if (NoRelatedSym is not null && NoRelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, NoRelatedSym.Length);
				for (int i = 0; i < NoRelatedSym.Length; i++)
				{
					((IFixEncoder)NoRelatedSym[i]).Encode(writer);
				}
			}
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
			TotalNumSecurities = view.GetInt32(393);
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				NoRelatedSym = new DerivativeSecurityListNoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedSym[i] = new();
					((IFixParser)NoRelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
				}
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
				case "TotalNumSecurities":
					value = TotalNumSecurities;
					break;
				case "NoRelatedSym":
					value = NoRelatedSym;
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
			TotalNumSecurities = null;
			NoRelatedSym = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
