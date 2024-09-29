using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RequestedRiskLimitTypesGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1668, Offset = 0, Required = false)]
		public NoRequestedRiskLimitType[]? NoRequestedRiskLimitType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRequestedRiskLimitType is not null && NoRequestedRiskLimitType.Length != 0)
			{
				writer.WriteWholeNumber(1668, NoRequestedRiskLimitType.Length);
				for (int i = 0; i < NoRequestedRiskLimitType.Length; i++)
				{
					((IFixEncoder)NoRequestedRiskLimitType[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRequestedRiskLimitType") is IMessageView viewNoRequestedRiskLimitType)
			{
				var count = viewNoRequestedRiskLimitType.GroupCount();
				NoRequestedRiskLimitType = new NoRequestedRiskLimitType[count];
				for (int i = 0; i < count; i++)
				{
					NoRequestedRiskLimitType[i] = new();
					((IFixParser)NoRequestedRiskLimitType[i]).Parse(viewNoRequestedRiskLimitType.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRequestedRiskLimitType":
					value = NoRequestedRiskLimitType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRequestedRiskLimitType = null;
		}
	}
}
