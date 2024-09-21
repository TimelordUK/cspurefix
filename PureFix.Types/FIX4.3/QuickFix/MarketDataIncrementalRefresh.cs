using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("X", FixVersion.FIX43)]
	public sealed partial class MarketDataIncrementalRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = false)]
		public string? MDReqID {get; set;}
		
		[Group(NoOfTag = 268, Offset = 2, Required = true)]
		public NoMDEntries[]? NoMDEntries {get; set;}
		
		[Component(Offset = 3, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& NoMDEntries is not null && FixValidator.IsValid(NoMDEntries, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
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
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			MDReqID = view.GetString(262);
			if (view.GetView("NoMDEntries") is IMessageView viewNoMDEntries)
			{
				var count = viewNoMDEntries.GroupCount();
				NoMDEntries = new NoMDEntries[count];
				for (int i = 0; i < count; i++)
				{
					NoMDEntries[i] = new();
					((IFixParser)NoMDEntries[i]).Parse(viewNoMDEntries.GetGroupInstance(i));
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
				case "MDReqID":
					value = MDReqID;
					break;
				case "NoMDEntries":
					value = NoMDEntries;
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
			MDReqID = null;
			NoMDEntries = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
