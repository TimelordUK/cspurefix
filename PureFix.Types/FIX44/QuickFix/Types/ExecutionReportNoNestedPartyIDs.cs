using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ExecutionReportNoNestedPartyIDs : IFixGroup
	{
		[TagDetails(Tag = 524, Type = TagType.String, Offset = 0, Required = false)]
		public string? NestedPartyID {get; set;}
		
		[TagDetails(Tag = 525, Type = TagType.String, Offset = 1, Required = false)]
		public string? NestedPartyIDSource {get; set;}
		
		[TagDetails(Tag = 538, Type = TagType.Int, Offset = 2, Required = false)]
		public int? NestedPartyRole {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public NstdPtysSubGrpComponent? NstdPtysSubGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NestedPartyID is not null) writer.WriteString(524, NestedPartyID);
			if (NestedPartyIDSource is not null) writer.WriteString(525, NestedPartyIDSource);
			if (NestedPartyRole is not null) writer.WriteWholeNumber(538, NestedPartyRole.Value);
			if (NstdPtysSubGrp is not null) ((IFixEncoder)NstdPtysSubGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			NestedPartyID = view.GetString(524);
			NestedPartyIDSource = view.GetString(525);
			NestedPartyRole = view.GetInt32(538);
			if (view.GetView("NstdPtysSubGrp") is IMessageView viewNstdPtysSubGrp)
			{
				NstdPtysSubGrp = new();
				((IFixParser)NstdPtysSubGrp).Parse(viewNstdPtysSubGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NestedPartyID":
					value = NestedPartyID;
					break;
				case "NestedPartyIDSource":
					value = NestedPartyIDSource;
					break;
				case "NestedPartyRole":
					value = NestedPartyRole;
					break;
				case "NstdPtysSubGrp":
					value = NstdPtysSubGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NestedPartyID = null;
			NestedPartyIDSource = null;
			NestedPartyRole = null;
			((IFixReset?)NstdPtysSubGrp)?.Reset();
		}
	}
}
