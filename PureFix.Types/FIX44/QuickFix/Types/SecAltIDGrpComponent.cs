using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 454, Offset = 0, Required = false)]
		public IOIInstrumentSecAltIDGrpNoSecurityAltID[]? NoSecurityAltID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSecurityAltID is not null && NoSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(454, NoSecurityAltID.Length);
				for (int i = 0; i < NoSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoSecurityAltID[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSecurityAltID") is IMessageView viewNoSecurityAltID)
			{
				var count = viewNoSecurityAltID.GroupCount();
				NoSecurityAltID = new IOIInstrumentSecAltIDGrpNoSecurityAltID[count];
				for (int i = 0; i < count; i++)
				{
					NoSecurityAltID[i] = new();
					((IFixParser)NoSecurityAltID[i]).Parse(viewNoSecurityAltID.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSecurityAltID":
					value = NoSecurityAltID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSecurityAltID = null;
		}
	}
}
