using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class MassQuoteNoQuoteSets : IFixGroup
	{
		[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = true)]
		public string? QuoteSetID {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[TagDetails(Tag = 367, Type = TagType.UtcTimestamp, Offset = 2, Required = false)]
		public DateTime? QuoteSetValidUntilTime {get; set;}
		
		[TagDetails(Tag = 304, Type = TagType.Int, Offset = 3, Required = true)]
		public int? TotQuoteEntries {get; set;}
		
		[Group(NoOfTag = 295, Offset = 4, Required = true)]
		public MassQuoteNoQuoteEntries[]? NoQuoteEntries {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				QuoteSetID is not null
				&& TotQuoteEntries is not null
				&& NoQuoteEntries is not null && FixValidator.IsValid(NoQuoteEntries, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (QuoteSetID is not null) writer.WriteString(302, QuoteSetID);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (QuoteSetValidUntilTime is not null) writer.WriteUtcTimeStamp(367, QuoteSetValidUntilTime.Value);
			if (TotQuoteEntries is not null) writer.WriteWholeNumber(304, TotQuoteEntries.Value);
			if (NoQuoteEntries is not null && NoQuoteEntries.Length != 0)
			{
				writer.WriteWholeNumber(295, NoQuoteEntries.Length);
				for (int i = 0; i < NoQuoteEntries.Length; i++)
				{
					((IFixEncoder)NoQuoteEntries[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			QuoteSetID = view.GetString(302);
			if (view.GetView("UnderlyingInstrument") is IMessageView viewUnderlyingInstrument)
			{
				UnderlyingInstrument = new();
				((IFixParser)UnderlyingInstrument).Parse(viewUnderlyingInstrument);
			}
			QuoteSetValidUntilTime = view.GetDateTime(367);
			TotQuoteEntries = view.GetInt32(304);
			if (view.GetView("NoQuoteEntries") is IMessageView viewNoQuoteEntries)
			{
				var count = viewNoQuoteEntries.GroupCount();
				NoQuoteEntries = new MassQuoteNoQuoteEntries[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteEntries[i] = new();
					((IFixParser)NoQuoteEntries[i]).Parse(viewNoQuoteEntries.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "QuoteSetID":
					value = QuoteSetID;
					break;
				case "UnderlyingInstrument":
					value = UnderlyingInstrument;
					break;
				case "QuoteSetValidUntilTime":
					value = QuoteSetValidUntilTime;
					break;
				case "TotQuoteEntries":
					value = TotQuoteEntries;
					break;
				case "NoQuoteEntries":
					value = NoQuoteEntries;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			QuoteSetID = null;
			((IFixReset?)UnderlyingInstrument)?.Reset();
			QuoteSetValidUntilTime = null;
			TotQuoteEntries = null;
			NoQuoteEntries = null;
		}
	}
}
