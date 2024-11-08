using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoUnderlyingLegSecurityAltID : IFixGroup
	{
		[TagDetails(Tag = 1335, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingLegSecurityAltID {get; set;}
		
		[TagDetails(Tag = 1336, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingLegSecurityAltIDSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingLegSecurityAltID is not null) writer.WriteString(1335, UnderlyingLegSecurityAltID);
			if (UnderlyingLegSecurityAltIDSource is not null) writer.WriteString(1336, UnderlyingLegSecurityAltIDSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingLegSecurityAltID = view.GetString(1335);
			UnderlyingLegSecurityAltIDSource = view.GetString(1336);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingLegSecurityAltID":
					value = UnderlyingLegSecurityAltID;
					break;
				case "UnderlyingLegSecurityAltIDSource":
					value = UnderlyingLegSecurityAltIDSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingLegSecurityAltID = null;
			UnderlyingLegSecurityAltIDSource = null;
		}
	}
}
