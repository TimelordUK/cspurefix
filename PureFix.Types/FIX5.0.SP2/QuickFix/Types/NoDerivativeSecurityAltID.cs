using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoDerivativeSecurityAltID : IFixGroup
	{
		[TagDetails(Tag = 1219, Type = TagType.String, Offset = 0, Required = false)]
		public string? DerivativeSecurityAltID {get; set;}
		
		[TagDetails(Tag = 1220, Type = TagType.String, Offset = 1, Required = false)]
		public string? DerivativeSecurityAltIDSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DerivativeSecurityAltID is not null) writer.WriteString(1219, DerivativeSecurityAltID);
			if (DerivativeSecurityAltIDSource is not null) writer.WriteString(1220, DerivativeSecurityAltIDSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DerivativeSecurityAltID = view.GetString(1219);
			DerivativeSecurityAltIDSource = view.GetString(1220);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DerivativeSecurityAltID":
					value = DerivativeSecurityAltID;
					break;
				case "DerivativeSecurityAltIDSource":
					value = DerivativeSecurityAltIDSource;
					break;
				default: return false;
			}
			return true;
		}
	}
}
