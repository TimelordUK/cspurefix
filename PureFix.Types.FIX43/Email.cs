using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("C", FixVersion.FIX43)]
	public sealed partial class Email : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 164, Type = TagType.String, Offset = 1, Required = true)]
		public string? EmailThreadID {get; set;}
		
		[TagDetails(Tag = 94, Type = TagType.String, Offset = 2, Required = true)]
		public string? EmailType {get; set;}
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 3, Required = false)]
		public DateTime? OrigTime {get; set;}
		
		[TagDetails(Tag = 147, Type = TagType.String, Offset = 4, Required = true)]
		public string? Subject {get; set;}
		
		[TagDetails(Tag = 356, Type = TagType.Length, Offset = 5, Required = false)]
		public int? EncodedSubjectLen {get; set;}
		
		[TagDetails(Tag = 357, Type = TagType.RawData, Offset = 6, Required = false)]
		public byte[]? EncodedSubject {get; set;}
		
		public sealed partial class NoRoutingIDs : IFixGroup
		{
			[TagDetails(Tag = 216, Type = TagType.Int, Offset = 0, Required = false)]
			public int? RoutingType {get; set;}
			
			[TagDetails(Tag = 217, Type = TagType.String, Offset = 1, Required = false)]
			public string? RoutingID {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (RoutingType is not null) writer.WriteWholeNumber(216, RoutingType.Value);
				if (RoutingID is not null) writer.WriteString(217, RoutingID);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				RoutingType = view.GetInt32(216);
				RoutingID = view.GetString(217);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "RoutingType":
					{
						value = RoutingType;
						break;
					}
					case "RoutingID":
					{
						value = RoutingID;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				RoutingType = null;
				RoutingID = null;
			}
		}
		[Group(NoOfTag = 215, Offset = 7, Required = false)]
		public NoRoutingIDs[]? RoutingIDs {get; set;}
		
		public sealed partial class NoRelatedSym : IFixGroup
		{
			[Component(Offset = 0, Required = false)]
			public Instrument? Instrument {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				if (view.GetView("Instrument") is IMessageView viewInstrument)
				{
					Instrument = new();
					((IFixParser)Instrument).Parse(viewInstrument);
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "Instrument":
					{
						value = Instrument;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				((IFixReset?)Instrument)?.Reset();
			}
		}
		[Group(NoOfTag = 146, Offset = 8, Required = false)]
		public NoRelatedSym[]? RelatedSym {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 9, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 10, Required = false)]
		public string? ClOrdID {get; set;}
		
		public sealed partial class LinesOfText : IFixGroup
		{
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 0, Required = true)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 1, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 2, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				Text = view.GetString(58);
				EncodedTextLen = view.GetInt32(354);
				EncodedText = view.GetByteArray(355);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "Text":
					{
						value = Text;
						break;
					}
					case "EncodedTextLen":
					{
						value = EncodedTextLen;
						break;
					}
					case "EncodedText":
					{
						value = EncodedText;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
			}
		}
		[Group(NoOfTag = 33, Offset = 11, Required = true)]
		public LinesOfText[]? LinesOfTextItems {get; set;}
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 12, Required = false)]
		public int? RawDataLength {get; set;}
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 13, Required = false)]
		public byte[]? RawData {get; set;}
		
		[Component(Offset = 14, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (EmailThreadID is not null) writer.WriteString(164, EmailThreadID);
			if (EmailType is not null) writer.WriteString(94, EmailType);
			if (OrigTime is not null) writer.WriteUtcTimeStamp(42, OrigTime.Value);
			if (Subject is not null) writer.WriteString(147, Subject);
			if (EncodedSubjectLen is not null) writer.WriteWholeNumber(356, EncodedSubjectLen.Value);
			if (EncodedSubject is not null) writer.WriteBuffer(357, EncodedSubject);
			if (RoutingIDs is not null && RoutingIDs.Length != 0)
			{
				writer.WriteWholeNumber(215, RoutingIDs.Length);
				for (int i = 0; i < RoutingIDs.Length; i++)
				{
					((IFixEncoder)RoutingIDs[i]).Encode(writer);
				}
			}
			if (RelatedSym is not null && RelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, RelatedSym.Length);
				for (int i = 0; i < RelatedSym.Length; i++)
				{
					((IFixEncoder)RelatedSym[i]).Encode(writer);
				}
			}
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (LinesOfTextItems is not null && LinesOfTextItems.Length != 0)
			{
				writer.WriteWholeNumber(33, LinesOfTextItems.Length);
				for (int i = 0; i < LinesOfTextItems.Length; i++)
				{
					((IFixEncoder)LinesOfTextItems[i]).Encode(writer);
				}
			}
			if (RawDataLength is not null) writer.WriteWholeNumber(95, RawDataLength.Value);
			if (RawData is not null) writer.WriteBuffer(96, RawData);
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
			EmailThreadID = view.GetString(164);
			EmailType = view.GetString(94);
			OrigTime = view.GetDateTime(42);
			Subject = view.GetString(147);
			EncodedSubjectLen = view.GetInt32(356);
			EncodedSubject = view.GetByteArray(357);
			if (view.GetView("NoRoutingIDs") is IMessageView viewNoRoutingIDs)
			{
				var count = viewNoRoutingIDs.GroupCount();
				RoutingIDs = new NoRoutingIDs[count];
				for (int i = 0; i < count; i++)
				{
					RoutingIDs[i] = new();
					((IFixParser)RoutingIDs[i]).Parse(viewNoRoutingIDs.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				RelatedSym = new NoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					RelatedSym[i] = new();
					((IFixParser)RelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
				}
			}
			OrderID = view.GetString(37);
			ClOrdID = view.GetString(11);
			if (view.GetView("LinesOfText") is IMessageView viewLinesOfText)
			{
				var count = viewLinesOfText.GroupCount();
				LinesOfTextItems = new LinesOfText[count];
				for (int i = 0; i < count; i++)
				{
					LinesOfTextItems[i] = new();
					((IFixParser)LinesOfTextItems[i]).Parse(viewLinesOfText.GetGroupInstance(i));
				}
			}
			RawDataLength = view.GetInt32(95);
			RawData = view.GetByteArray(96);
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
				{
					value = StandardHeader;
					break;
				}
				case "EmailThreadID":
				{
					value = EmailThreadID;
					break;
				}
				case "EmailType":
				{
					value = EmailType;
					break;
				}
				case "OrigTime":
				{
					value = OrigTime;
					break;
				}
				case "Subject":
				{
					value = Subject;
					break;
				}
				case "EncodedSubjectLen":
				{
					value = EncodedSubjectLen;
					break;
				}
				case "EncodedSubject":
				{
					value = EncodedSubject;
					break;
				}
				case "NoRoutingIDs":
				{
					value = RoutingIDs;
					break;
				}
				case "NoRelatedSym":
				{
					value = RelatedSym;
					break;
				}
				case "OrderID":
				{
					value = OrderID;
					break;
				}
				case "ClOrdID":
				{
					value = ClOrdID;
					break;
				}
				case "LinesOfText":
				{
					value = LinesOfTextItems;
					break;
				}
				case "RawDataLength":
				{
					value = RawDataLength;
					break;
				}
				case "RawData":
				{
					value = RawData;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			EmailThreadID = null;
			EmailType = null;
			OrigTime = null;
			Subject = null;
			EncodedSubjectLen = null;
			EncodedSubject = null;
			RoutingIDs = null;
			RelatedSym = null;
			OrderID = null;
			ClOrdID = null;
			LinesOfTextItems = null;
			RawDataLength = null;
			RawData = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
