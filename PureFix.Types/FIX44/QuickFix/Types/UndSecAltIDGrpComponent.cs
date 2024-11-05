using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UndSecAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 457, Offset = 0, Required = false)]
		public IOIUndInstrmtGrpNoUnderlyingsUnderlyingInstrumentUndSecAltIDGrpNoUnderlyingSecurityAltID[]? NoUnderlyingSecurityAltID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingSecurityAltID is not null && NoUnderlyingSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(457, NoUnderlyingSecurityAltID.Length);
				for (int i = 0; i < NoUnderlyingSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoUnderlyingSecurityAltID[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingSecurityAltID") is IMessageView viewNoUnderlyingSecurityAltID)
			{
				var count = viewNoUnderlyingSecurityAltID.GroupCount();
				NoUnderlyingSecurityAltID = new IOIUndInstrmtGrpNoUnderlyingsUnderlyingInstrumentUndSecAltIDGrpNoUnderlyingSecurityAltID[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingSecurityAltID[i] = new();
					((IFixParser)NoUnderlyingSecurityAltID[i]).Parse(viewNoUnderlyingSecurityAltID.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingSecurityAltID":
					value = NoUnderlyingSecurityAltID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingSecurityAltID = null;
		}
	}
}
