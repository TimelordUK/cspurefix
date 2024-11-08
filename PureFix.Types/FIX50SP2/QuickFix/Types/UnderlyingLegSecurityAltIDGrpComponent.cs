using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingLegSecurityAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1334, Offset = 0, Required = false)]
		public TradeCaptureReportNoUnderlyingLegSecurityAltID[]? NoUnderlyingLegSecurityAltID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingLegSecurityAltID is not null && NoUnderlyingLegSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(1334, NoUnderlyingLegSecurityAltID.Length);
				for (int i = 0; i < NoUnderlyingLegSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoUnderlyingLegSecurityAltID[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingLegSecurityAltID") is IMessageView viewNoUnderlyingLegSecurityAltID)
			{
				var count = viewNoUnderlyingLegSecurityAltID.GroupCount();
				NoUnderlyingLegSecurityAltID = new TradeCaptureReportNoUnderlyingLegSecurityAltID[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingLegSecurityAltID[i] = new();
					((IFixParser)NoUnderlyingLegSecurityAltID[i]).Parse(viewNoUnderlyingLegSecurityAltID.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingLegSecurityAltID":
					value = NoUnderlyingLegSecurityAltID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingLegSecurityAltID = null;
		}
	}
}
