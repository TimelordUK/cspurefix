using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AH", FixVersion.FIX44)]
	public sealed partial class RFQRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 1, Required = true)]
		public string? RFQReqID {get; set;}
		
		[Component(Offset = 2, Required = true)]
		public RFQReqGrpComponent? RFQReqGrp {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& RFQReqID is not null
				&& RFQReqGrp is not null && ((IFixValidator)RFQReqGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (RFQReqID is not null) writer.WriteString(644, RFQReqID);
			if (RFQReqGrp is not null) ((IFixEncoder)RFQReqGrp).Encode(writer);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
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
			RFQReqID = view.GetString(644);
			if (view.GetView("RFQReqGrp") is IMessageView viewRFQReqGrp)
			{
				RFQReqGrp = new();
				((IFixParser)RFQReqGrp).Parse(viewRFQReqGrp);
			}
			SubscriptionRequestType = view.GetString(263);
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
				case "RFQReqID":
					value = RFQReqID;
					break;
				case "RFQReqGrp":
					value = RFQReqGrp;
					break;
				case "SubscriptionRequestType":
					value = SubscriptionRequestType;
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
			RFQReqID = null;
			((IFixReset?)RFQReqGrp)?.Reset();
			SubscriptionRequestType = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
