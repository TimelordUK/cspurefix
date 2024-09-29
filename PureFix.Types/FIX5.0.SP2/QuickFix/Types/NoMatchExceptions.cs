using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoMatchExceptions : IFixGroup
	{
		[TagDetails(Tag = 2773, Type = TagType.Int, Offset = 0, Required = false)]
		public int? MatchExceptionType {get; set;}
		
		[TagDetails(Tag = 2774, Type = TagType.Int, Offset = 1, Required = false)]
		public int? MatchExceptionElementType {get; set;}
		
		[TagDetails(Tag = 2775, Type = TagType.String, Offset = 2, Required = false)]
		public string? MatchExceptionElementName {get; set;}
		
		[TagDetails(Tag = 2776, Type = TagType.String, Offset = 3, Required = false)]
		public string? MatchExceptionAllocValue {get; set;}
		
		[TagDetails(Tag = 2777, Type = TagType.String, Offset = 4, Required = false)]
		public string? MatchExceptionConfirmValue {get; set;}
		
		[TagDetails(Tag = 2778, Type = TagType.Float, Offset = 5, Required = false)]
		public double? MatchExceptionToleranceValue {get; set;}
		
		[TagDetails(Tag = 2779, Type = TagType.Int, Offset = 6, Required = false)]
		public int? MatchExceptionToleranceValueType {get; set;}
		
		[TagDetails(Tag = 2780, Type = TagType.String, Offset = 7, Required = false)]
		public string? MatchExceptionText {get; set;}
		
		[TagDetails(Tag = 2797, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 2798)]
		public int? EncodedMatchExceptionTextLen {get; set;}
		
		[TagDetails(Tag = 2798, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 2797)]
		public byte[]? EncodedMatchExceptionText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MatchExceptionType is not null) writer.WriteWholeNumber(2773, MatchExceptionType.Value);
			if (MatchExceptionElementType is not null) writer.WriteWholeNumber(2774, MatchExceptionElementType.Value);
			if (MatchExceptionElementName is not null) writer.WriteString(2775, MatchExceptionElementName);
			if (MatchExceptionAllocValue is not null) writer.WriteString(2776, MatchExceptionAllocValue);
			if (MatchExceptionConfirmValue is not null) writer.WriteString(2777, MatchExceptionConfirmValue);
			if (MatchExceptionToleranceValue is not null) writer.WriteNumber(2778, MatchExceptionToleranceValue.Value);
			if (MatchExceptionToleranceValueType is not null) writer.WriteWholeNumber(2779, MatchExceptionToleranceValueType.Value);
			if (MatchExceptionText is not null) writer.WriteString(2780, MatchExceptionText);
			if (EncodedMatchExceptionText is not null)
			{
				writer.WriteWholeNumber(2797, EncodedMatchExceptionText.Length);
				writer.WriteBuffer(2798, EncodedMatchExceptionText);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MatchExceptionType = view.GetInt32(2773);
			MatchExceptionElementType = view.GetInt32(2774);
			MatchExceptionElementName = view.GetString(2775);
			MatchExceptionAllocValue = view.GetString(2776);
			MatchExceptionConfirmValue = view.GetString(2777);
			MatchExceptionToleranceValue = view.GetDouble(2778);
			MatchExceptionToleranceValueType = view.GetInt32(2779);
			MatchExceptionText = view.GetString(2780);
			EncodedMatchExceptionTextLen = view.GetInt32(2797);
			EncodedMatchExceptionText = view.GetByteArray(2798);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MatchExceptionType":
					value = MatchExceptionType;
					break;
				case "MatchExceptionElementType":
					value = MatchExceptionElementType;
					break;
				case "MatchExceptionElementName":
					value = MatchExceptionElementName;
					break;
				case "MatchExceptionAllocValue":
					value = MatchExceptionAllocValue;
					break;
				case "MatchExceptionConfirmValue":
					value = MatchExceptionConfirmValue;
					break;
				case "MatchExceptionToleranceValue":
					value = MatchExceptionToleranceValue;
					break;
				case "MatchExceptionToleranceValueType":
					value = MatchExceptionToleranceValueType;
					break;
				case "MatchExceptionText":
					value = MatchExceptionText;
					break;
				case "EncodedMatchExceptionTextLen":
					value = EncodedMatchExceptionTextLen;
					break;
				case "EncodedMatchExceptionText":
					value = EncodedMatchExceptionText;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MatchExceptionType = null;
			MatchExceptionElementType = null;
			MatchExceptionElementName = null;
			MatchExceptionAllocValue = null;
			MatchExceptionConfirmValue = null;
			MatchExceptionToleranceValue = null;
			MatchExceptionToleranceValueType = null;
			MatchExceptionText = null;
			EncodedMatchExceptionTextLen = null;
			EncodedMatchExceptionText = null;
		}
	}
}
