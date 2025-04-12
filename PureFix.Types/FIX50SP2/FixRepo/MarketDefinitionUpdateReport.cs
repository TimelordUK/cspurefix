using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("MarketDefinitionUpdateReport", FixVersion.FIX50SP2)]
	public sealed partial class MarketDefinitionUpdateReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 1394, Type = TagType.String, Offset = 2, Required = true)]
		public string? MarketReportID {get; set;}
		
		[TagDetails(Tag = 1393, Type = TagType.String, Offset = 3, Required = false)]
		public string? MarketReqID {get; set;}
		
		[TagDetails(Tag = 1395, Type = TagType.String, Offset = 4, Required = false)]
		public string? MarketUpdateAction {get; set;}
		
		[TagDetails(Tag = 1301, Type = TagType.String, Offset = 5, Required = true)]
		public string? MarketID {get; set;}
		
		[TagDetails(Tag = 1300, Type = TagType.String, Offset = 6, Required = false)]
		public string? MarketSegmentID {get; set;}
		
		[TagDetails(Tag = 1396, Type = TagType.String, Offset = 7, Required = false)]
		public string? MarketSegmentDesc {get; set;}
		
		[TagDetails(Tag = 1397, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 1398)]
		public int? EncodedMktSegmDescLen {get; set;}
		
		[TagDetails(Tag = 1398, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 1397)]
		public byte[]? EncodedMktSegmDesc {get; set;}
		
		[TagDetails(Tag = 1325, Type = TagType.String, Offset = 10, Required = false)]
		public string? ParentMktSegmID {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 11, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 12, Required = false)]
		public BaseTradingRulesComponent? BaseTradingRules {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 13, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 14, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 17, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& MarketReportID is not null
				&& MarketID is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (MarketReportID is not null) writer.WriteString(1394, MarketReportID);
			if (MarketReqID is not null) writer.WriteString(1393, MarketReqID);
			if (MarketUpdateAction is not null) writer.WriteString(1395, MarketUpdateAction);
			if (MarketID is not null) writer.WriteString(1301, MarketID);
			if (MarketSegmentID is not null) writer.WriteString(1300, MarketSegmentID);
			if (MarketSegmentDesc is not null) writer.WriteString(1396, MarketSegmentDesc);
			if (EncodedMktSegmDesc is not null)
			{
				writer.WriteWholeNumber(1397, EncodedMktSegmDesc.Length);
				writer.WriteBuffer(1398, EncodedMktSegmDesc);
			}
			if (ParentMktSegmID is not null) writer.WriteString(1325, ParentMktSegmID);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (BaseTradingRules is not null) ((IFixEncoder)BaseTradingRules).Encode(writer);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
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
			if (view.GetView("ApplicationSequenceControl") is IMessageView viewApplicationSequenceControl)
			{
				ApplicationSequenceControl = new();
				((IFixParser)ApplicationSequenceControl).Parse(viewApplicationSequenceControl);
			}
			MarketReportID = view.GetString(1394);
			MarketReqID = view.GetString(1393);
			MarketUpdateAction = view.GetString(1395);
			MarketID = view.GetString(1301);
			MarketSegmentID = view.GetString(1300);
			MarketSegmentDesc = view.GetString(1396);
			EncodedMktSegmDescLen = view.GetInt32(1397);
			EncodedMktSegmDesc = view.GetByteArray(1398);
			ParentMktSegmID = view.GetString(1325);
			Currency = view.GetString(15);
			if (view.GetView("BaseTradingRules") is IMessageView viewBaseTradingRules)
			{
				BaseTradingRules = new();
				((IFixParser)BaseTradingRules).Parse(viewBaseTradingRules);
			}
			TransactTime = view.GetDateTime(60);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
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
				case "MarketReportID":
					value = MarketReportID;
					break;
				case "MarketReqID":
					value = MarketReqID;
					break;
				case "MarketUpdateAction":
					value = MarketUpdateAction;
					break;
				case "MarketID":
					value = MarketID;
					break;
				case "MarketSegmentID":
					value = MarketSegmentID;
					break;
				case "MarketSegmentDesc":
					value = MarketSegmentDesc;
					break;
				case "EncodedMktSegmDescLen":
					value = EncodedMktSegmDescLen;
					break;
				case "EncodedMktSegmDesc":
					value = EncodedMktSegmDesc;
					break;
				case "ParentMktSegmID":
					value = ParentMktSegmID;
					break;
				case "Currency":
					value = Currency;
					break;
				case "BaseTradingRules":
					value = BaseTradingRules;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
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
			MarketReportID = null;
			MarketReqID = null;
			MarketUpdateAction = null;
			MarketID = null;
			MarketSegmentID = null;
			MarketSegmentDesc = null;
			EncodedMktSegmDescLen = null;
			EncodedMktSegmDesc = null;
			ParentMktSegmID = null;
			Currency = null;
			((IFixReset?)BaseTradingRules)?.Reset();
			TransactTime = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
