using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ApplicationMessageRequestAckNoApplIDs : IFixGroup
	{
		[TagDetails(Tag = 1355, Type = TagType.String, Offset = 0, Required = false)]
		public string? RefApplID {get; set;}
		
		[TagDetails(Tag = 1433, Type = TagType.String, Offset = 1, Required = false)]
		public string? RefApplReqID {get; set;}
		
		[TagDetails(Tag = 1182, Type = TagType.Int, Offset = 2, Required = false)]
		public int? ApplBegSeqNum {get; set;}
		
		[TagDetails(Tag = 1183, Type = TagType.Int, Offset = 3, Required = false)]
		public int? ApplEndSeqNum {get; set;}
		
		[TagDetails(Tag = 1357, Type = TagType.Int, Offset = 4, Required = false)]
		public int? RefApplLastSeqNum {get; set;}
		
		[TagDetails(Tag = 1354, Type = TagType.Int, Offset = 5, Required = false)]
		public int? ApplResponseError {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public NestedPartiesComponent? NestedParties {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RefApplID is not null) writer.WriteString(1355, RefApplID);
			if (RefApplReqID is not null) writer.WriteString(1433, RefApplReqID);
			if (ApplBegSeqNum is not null) writer.WriteWholeNumber(1182, ApplBegSeqNum.Value);
			if (ApplEndSeqNum is not null) writer.WriteWholeNumber(1183, ApplEndSeqNum.Value);
			if (RefApplLastSeqNum is not null) writer.WriteWholeNumber(1357, RefApplLastSeqNum.Value);
			if (ApplResponseError is not null) writer.WriteWholeNumber(1354, ApplResponseError.Value);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RefApplID = view.GetString(1355);
			RefApplReqID = view.GetString(1433);
			ApplBegSeqNum = view.GetInt32(1182);
			ApplEndSeqNum = view.GetInt32(1183);
			RefApplLastSeqNum = view.GetInt32(1357);
			ApplResponseError = view.GetInt32(1354);
			if (view.GetView("NestedParties") is IMessageView viewNestedParties)
			{
				NestedParties = new();
				((IFixParser)NestedParties).Parse(viewNestedParties);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RefApplID":
					value = RefApplID;
					break;
				case "RefApplReqID":
					value = RefApplReqID;
					break;
				case "ApplBegSeqNum":
					value = ApplBegSeqNum;
					break;
				case "ApplEndSeqNum":
					value = ApplEndSeqNum;
					break;
				case "RefApplLastSeqNum":
					value = RefApplLastSeqNum;
					break;
				case "ApplResponseError":
					value = ApplResponseError;
					break;
				case "NestedParties":
					value = NestedParties;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RefApplID = null;
			RefApplReqID = null;
			ApplBegSeqNum = null;
			ApplEndSeqNum = null;
			RefApplLastSeqNum = null;
			ApplResponseError = null;
			((IFixReset?)NestedParties)?.Reset();
		}
	}
}
