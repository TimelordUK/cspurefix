using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExecutionReportNoRegulatoryTradeIDs : IFixGroup
	{
		[TagDetails(Tag = 1903, Type = TagType.String, Offset = 0, Required = false)]
		public string? RegulatoryTradeID {get; set;}
		
		[TagDetails(Tag = 1905, Type = TagType.String, Offset = 1, Required = false)]
		public string? RegulatoryTradeIDSource {get; set;}
		
		[TagDetails(Tag = 1904, Type = TagType.Int, Offset = 2, Required = false)]
		public int? RegulatoryTradeIDEvent {get; set;}
		
		[TagDetails(Tag = 1906, Type = TagType.Int, Offset = 3, Required = false)]
		public int? RegulatoryTradeIDType {get; set;}
		
		[TagDetails(Tag = 2411, Type = TagType.String, Offset = 4, Required = false)]
		public string? RegulatoryLegRefID {get; set;}
		
		[TagDetails(Tag = 2397, Type = TagType.Int, Offset = 5, Required = false)]
		public int? RegulatoryTradeIDScope {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RegulatoryTradeID is not null) writer.WriteString(1903, RegulatoryTradeID);
			if (RegulatoryTradeIDSource is not null) writer.WriteString(1905, RegulatoryTradeIDSource);
			if (RegulatoryTradeIDEvent is not null) writer.WriteWholeNumber(1904, RegulatoryTradeIDEvent.Value);
			if (RegulatoryTradeIDType is not null) writer.WriteWholeNumber(1906, RegulatoryTradeIDType.Value);
			if (RegulatoryLegRefID is not null) writer.WriteString(2411, RegulatoryLegRefID);
			if (RegulatoryTradeIDScope is not null) writer.WriteWholeNumber(2397, RegulatoryTradeIDScope.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RegulatoryTradeID = view.GetString(1903);
			RegulatoryTradeIDSource = view.GetString(1905);
			RegulatoryTradeIDEvent = view.GetInt32(1904);
			RegulatoryTradeIDType = view.GetInt32(1906);
			RegulatoryLegRefID = view.GetString(2411);
			RegulatoryTradeIDScope = view.GetInt32(2397);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RegulatoryTradeID":
					value = RegulatoryTradeID;
					break;
				case "RegulatoryTradeIDSource":
					value = RegulatoryTradeIDSource;
					break;
				case "RegulatoryTradeIDEvent":
					value = RegulatoryTradeIDEvent;
					break;
				case "RegulatoryTradeIDType":
					value = RegulatoryTradeIDType;
					break;
				case "RegulatoryLegRefID":
					value = RegulatoryLegRefID;
					break;
				case "RegulatoryTradeIDScope":
					value = RegulatoryTradeIDScope;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RegulatoryTradeID = null;
			RegulatoryTradeIDSource = null;
			RegulatoryTradeIDEvent = null;
			RegulatoryTradeIDType = null;
			RegulatoryLegRefID = null;
			RegulatoryTradeIDScope = null;
		}
	}
}
