using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class NoLegSecurityAltID : IFixGroup
	{
		[TagDetails(Tag = 605, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegSecurityAltID {get; set;}
		
		[TagDetails(Tag = 606, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegSecurityAltIDSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegSecurityAltID is not null) writer.WriteString(605, LegSecurityAltID);
			if (LegSecurityAltIDSource is not null) writer.WriteString(606, LegSecurityAltIDSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegSecurityAltID = view.GetString(605);
			LegSecurityAltIDSource = view.GetString(606);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegSecurityAltID":
					value = LegSecurityAltID;
					break;
				case "LegSecurityAltIDSource":
					value = LegSecurityAltIDSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegSecurityAltID = null;
			LegSecurityAltIDSource = null;
		}
	}
}
