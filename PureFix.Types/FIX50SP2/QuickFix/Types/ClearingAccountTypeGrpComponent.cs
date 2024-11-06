using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ClearingAccountTypeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1918, Offset = 0, Required = false)]
		public SecurityListNoClearingAccountTypes[]? NoClearingAccountTypes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoClearingAccountTypes is not null && NoClearingAccountTypes.Length != 0)
			{
				writer.WriteWholeNumber(1918, NoClearingAccountTypes.Length);
				for (int i = 0; i < NoClearingAccountTypes.Length; i++)
				{
					((IFixEncoder)NoClearingAccountTypes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoClearingAccountTypes") is IMessageView viewNoClearingAccountTypes)
			{
				var count = viewNoClearingAccountTypes.GroupCount();
				NoClearingAccountTypes = new SecurityListNoClearingAccountTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoClearingAccountTypes[i] = new();
					((IFixParser)NoClearingAccountTypes[i]).Parse(viewNoClearingAccountTypes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoClearingAccountTypes":
					value = NoClearingAccountTypes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoClearingAccountTypes = null;
		}
	}
}
