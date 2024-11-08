using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AllocationInstructionNoAllocRegulatoryTradeIDs : IFixGroup
	{
		[TagDetails(Tag = 1909, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocRegulatoryTradeID {get; set;}
		
		[TagDetails(Tag = 1910, Type = TagType.String, Offset = 1, Required = false)]
		public string? AllocRegulatoryTradeIDSource {get; set;}
		
		[TagDetails(Tag = 1911, Type = TagType.Int, Offset = 2, Required = false)]
		public int? AllocRegulatoryTradeIDEvent {get; set;}
		
		[TagDetails(Tag = 1912, Type = TagType.Int, Offset = 3, Required = false)]
		public int? AllocRegulatoryTradeIDType {get; set;}
		
		[TagDetails(Tag = 2406, Type = TagType.String, Offset = 4, Required = false)]
		public string? AllocRegulatoryLegRefID {get; set;}
		
		[TagDetails(Tag = 2399, Type = TagType.Int, Offset = 5, Required = false)]
		public int? AllocRegulatoryTradeIDScope {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AllocRegulatoryTradeID is not null) writer.WriteString(1909, AllocRegulatoryTradeID);
			if (AllocRegulatoryTradeIDSource is not null) writer.WriteString(1910, AllocRegulatoryTradeIDSource);
			if (AllocRegulatoryTradeIDEvent is not null) writer.WriteWholeNumber(1911, AllocRegulatoryTradeIDEvent.Value);
			if (AllocRegulatoryTradeIDType is not null) writer.WriteWholeNumber(1912, AllocRegulatoryTradeIDType.Value);
			if (AllocRegulatoryLegRefID is not null) writer.WriteString(2406, AllocRegulatoryLegRefID);
			if (AllocRegulatoryTradeIDScope is not null) writer.WriteWholeNumber(2399, AllocRegulatoryTradeIDScope.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AllocRegulatoryTradeID = view.GetString(1909);
			AllocRegulatoryTradeIDSource = view.GetString(1910);
			AllocRegulatoryTradeIDEvent = view.GetInt32(1911);
			AllocRegulatoryTradeIDType = view.GetInt32(1912);
			AllocRegulatoryLegRefID = view.GetString(2406);
			AllocRegulatoryTradeIDScope = view.GetInt32(2399);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "AllocRegulatoryTradeID":
					value = AllocRegulatoryTradeID;
					break;
				case "AllocRegulatoryTradeIDSource":
					value = AllocRegulatoryTradeIDSource;
					break;
				case "AllocRegulatoryTradeIDEvent":
					value = AllocRegulatoryTradeIDEvent;
					break;
				case "AllocRegulatoryTradeIDType":
					value = AllocRegulatoryTradeIDType;
					break;
				case "AllocRegulatoryLegRefID":
					value = AllocRegulatoryLegRefID;
					break;
				case "AllocRegulatoryTradeIDScope":
					value = AllocRegulatoryTradeIDScope;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			AllocRegulatoryTradeID = null;
			AllocRegulatoryTradeIDSource = null;
			AllocRegulatoryTradeIDEvent = null;
			AllocRegulatoryTradeIDType = null;
			AllocRegulatoryLegRefID = null;
			AllocRegulatoryTradeIDScope = null;
		}
	}
}
