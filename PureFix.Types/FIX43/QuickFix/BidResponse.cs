using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("l", FixVersion.FIX43)]
	public sealed partial class BidResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 1, Required = false)]
		public string? BidID {get; set;}
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2, Required = false)]
		public string? ClientBidID {get; set;}
		
		[Group(NoOfTag = 420, Offset = 3, Required = true)]
		public NoBidComponents[]? NoBidComponents {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& NoBidComponents is not null && FixValidator.IsValid(NoBidComponents, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (BidID is not null) writer.WriteString(390, BidID);
			if (ClientBidID is not null) writer.WriteString(391, ClientBidID);
			if (NoBidComponents is not null && NoBidComponents.Length != 0)
			{
				writer.WriteWholeNumber(420, NoBidComponents.Length);
				for (int i = 0; i < NoBidComponents.Length; i++)
				{
					((IFixEncoder)NoBidComponents[i]).Encode(writer);
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
			BidID = view.GetString(390);
			ClientBidID = view.GetString(391);
			if (view.GetView("NoBidComponents") is IMessageView viewNoBidComponents)
			{
				var count = viewNoBidComponents.GroupCount();
				NoBidComponents = new NoBidComponents[count];
				for (int i = 0; i < count; i++)
				{
					NoBidComponents[i] = new();
					((IFixParser)NoBidComponents[i]).Parse(viewNoBidComponents.GetGroupInstance(i));
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
				case "BidID":
					value = BidID;
					break;
				case "ClientBidID":
					value = ClientBidID;
					break;
				case "NoBidComponents":
					value = NoBidComponents;
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
			BidID = null;
			ClientBidID = null;
			NoBidComponents = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
