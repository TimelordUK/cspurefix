using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("m", FixVersion.FIX42)]
	public sealed partial class ListStrikePrice : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 422, Type = TagType.Int, Offset = 2, Required = true)]
		public int? TotNoStrikes {get; set;}
		
		[Group(NoOfTag = 428, Offset = 3, Required = true)]
		public NoStrikes[]? NoStrikes {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ListID is not null
				&& TotNoStrikes is not null
				&& NoStrikes is not null && FixValidator.IsValid(NoStrikes, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (TotNoStrikes is not null) writer.WriteWholeNumber(422, TotNoStrikes.Value);
			if (NoStrikes is not null && NoStrikes.Length != 0)
			{
				writer.WriteWholeNumber(428, NoStrikes.Length);
				for (int i = 0; i < NoStrikes.Length; i++)
				{
					((IFixEncoder)NoStrikes[i]).Encode(writer);
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
			ListID = view.GetString(66);
			TotNoStrikes = view.GetInt32(422);
			if (view.GetView("NoStrikes") is IMessageView viewNoStrikes)
			{
				var count = viewNoStrikes.GroupCount();
				NoStrikes = new NoStrikes[count];
				for (int i = 0; i < count; i++)
				{
					NoStrikes[i] = new();
					((IFixParser)NoStrikes[i]).Parse(viewNoStrikes.GetGroupInstance(i));
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
				case "ListID":
					value = ListID;
					break;
				case "TotNoStrikes":
					value = TotNoStrikes;
					break;
				case "NoStrikes":
					value = NoStrikes;
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
			ListID = null;
			TotNoStrikes = null;
			NoStrikes = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
