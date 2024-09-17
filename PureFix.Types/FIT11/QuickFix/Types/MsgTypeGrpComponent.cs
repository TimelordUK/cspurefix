using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIXT11.QuickFix.Types;

namespace PureFix.Types.FIXT11.QuickFix.Types
{
	public sealed partial class MsgTypeGrpComponent : IFixComponent
	{
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			return false;
		}
	}
}
