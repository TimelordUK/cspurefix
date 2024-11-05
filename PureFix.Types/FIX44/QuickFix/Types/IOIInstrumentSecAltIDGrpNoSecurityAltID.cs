using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class IOIInstrumentSecAltIDGrpNoSecurityAltID : IFixGroup
	{
		[TagDetails(Tag = 455, Type = TagType.String, Offset = 0, Required = false)]
		public string? SecurityAltID {get; set;}
		
		[TagDetails(Tag = 456, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecurityAltIDSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SecurityAltID is not null) writer.WriteString(455, SecurityAltID);
			if (SecurityAltIDSource is not null) writer.WriteString(456, SecurityAltIDSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			SecurityAltID = view.GetString(455);
			SecurityAltIDSource = view.GetString(456);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "SecurityAltID":
					value = SecurityAltID;
					break;
				case "SecurityAltIDSource":
					value = SecurityAltIDSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			SecurityAltID = null;
			SecurityAltIDSource = null;
		}
	}
}
